using AmindaFeed.Models;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Net;
using Microsoft.AspNetCore.Hosting.Server;
using System;
using AmindaFeed.Constants;
using System.Transactions;
using DeepL;

namespace AmindaFeed.Services
{
    public class HttpService : IHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<MatterhornProduct> GetMatterhornProduct(string productId)
        {
            MatterhornProduct res = new();
            
            var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            $"https://matterhorn-wholesale.com/B2BAPI/ITEMS/{productId}")
            {
                Headers =
                    {
                        { HeaderNames.Accept, "application/json" },
                        { HeaderNames.Authorization, "6df0d3a898" }
                    }
            };

            var httpClient = _httpClientFactory.CreateClient();

            httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                res = await httpResponseMessage.Content.ReadFromJsonAsync<MatterhornProduct>();
            }
            return res;
        }

        public Task<List<MatterhornProduct>> GetMatterhornProducts(List<string> productIds)
        {
            throw new NotImplementedException();
        }

        public async Task SetAmindaProductFromMatterhorn(string productId)
        {
            var product = await GetMatterhornProduct(productId);
            var amindaProd = await MatterhornAmindaMapper(product);

            //string xmlAmindaProduct = MySerializer<AmindaProducts>.Serialize(amindaProd);
            var settings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = true
            };

            XmlSerializer amindaSerialization = new XmlSerializer(amindaProd.GetType());
            StringBuilder xmlAmindaProduct = new StringBuilder();
            var amindaWriter = new StringWriter(xmlAmindaProduct);
            var xmlAmindaWriter = XmlWriter.Create(amindaWriter, settings);

            amindaSerialization.Serialize(xmlAmindaWriter, amindaProd);


            Console.WriteLine($"xmlAminda {xmlAmindaProduct}");
            //var httpContent = new StringContent(xmlAmindaProduct, Encoding.UTF8, "application/xml");



            var login = new Params()
            {
                ApiKey = "82dd476ee053ff7778397bf6155969eeca084a70"
            };


            XmlSerializer x = new XmlSerializer(login.GetType());
            StringBuilder output = new StringBuilder();
            var writer = new StringWriter(output);
            var xmlWriter = XmlWriter.Create(writer, settings);

            x.Serialize(xmlWriter, login);

            Console.WriteLine(output);

            var loginUrl = "https://api.unas.eu/shop/login";

            var httpClient = _httpClientFactory.CreateClient();

            HttpRequestMessage httpLoginRequestMessage = new HttpRequestMessage
            {

                Method = HttpMethod.Get,
                RequestUri = new Uri(loginUrl),

            };
            httpLoginRequestMessage.Headers.Add("Accept", "application/xml");
            httpLoginRequestMessage.Content = new StringContent(output.ToString(), Encoding.UTF8, "application/xml");

            string apiKey = "";
            await httpClient.SendAsync(httpLoginRequestMessage)
                  .ContinueWith(async responseTask =>
                  {
                      Console.WriteLine("Response: {0}", responseTask.Result);
                      apiKey = await responseTask.Result.Content.ReadAsStringAsync();
                  });


            Login loginResponse;

            XmlSerializer fromXmlToLogin = new XmlSerializer(typeof(Login));

            XmlDocument xmlApiKey = new XmlDocument();

            xmlApiKey.LoadXml(apiKey);

            using (TextReader reader = new StringReader(xmlApiKey.OuterXml))
            {
                var xmlReader = XmlReader.Create(reader);
                loginResponse = (Login)fromXmlToLogin.Deserialize(xmlReader);
            }

            Console.WriteLine($"login response token: {loginResponse.Token}");

            var url = "https://api.unas.eu/shop/setProduct";

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage
            {

                Method = HttpMethod.Post,
                RequestUri = new Uri(url),

            };
            httpRequestMessage.Headers.Add("Accept", "application/xml");
            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", loginResponse.Token);
            httpRequestMessage.Content = new StringContent(xmlAmindaProduct.ToString(), Encoding.UTF8, "application/xml");

            Console.WriteLine($"aminda product xml : {xmlAmindaProduct.ToString()}");

            await httpClient.SendAsync(httpRequestMessage)
                  .ContinueWith(async responseTask =>
                  {
                      Console.WriteLine("Response AmindaProd : {0}", responseTask.Result);
                  });

        }

        private async Task<AmindaProducts> MatterhornAmindaMapper(MatterhornProduct matterhornProduct)
        {
            var name = matterhornProduct.Name.Replace(matterhornProduct.Id.ToString(), matterhornProduct.Color).Replace("model","");
            var images = new List<Image>();
            var isImageFirst = true;
            var baseFolderPathToSave = "D:/Personal/Aminda/Termékek/Beszerzés/Matterhorn/";
            var imagePathToSave = "";
            foreach (var image in matterhornProduct.Images)
            {
                imagePathToSave = baseFolderPathToSave + $"{matterhornProduct.Name}/{name}.jpg";
                if (!Directory.Exists($"{baseFolderPathToSave}/{matterhornProduct.Name}"))
                {
                    Directory.CreateDirectory($"{baseFolderPathToSave}/{matterhornProduct.Name}");
                }

                //var httpClient = _httpClientFactory.CreateClient();
                //byte[] fileBytes = await httpClient.GetByteArrayAsync(image);
                //File.WriteAllBytes(imagePathToSave, fileBytes);


                //using (WebClient client = new WebClient())
                //{
                //    client.DownloadFile(new Uri(image), imagePathToSave);
                //}
                images.Add(
                    new Image()
                    {
                        Type = isImageFirst ? "base" : "alt",
                        Import = new Import()
                        {
                            Url = image
                        }
                    }
                    );
                isImageFirst = false;
            }

            var parameters = new List<Param>
            {
                new Param()
                {
                    Id = 962692,
                    Type = "text",
                    Name = "brand",
                    Value = matterhornProduct.Brand
                },
                new Param()
                {
                    Id = 1865633,
                    Type = "text",
                    Name = "google_product_category",
                    Value = "2271"
                },
                new Param()
                {
                    Id = 2928445,
                    Type = "enum",
                    Name = "Várható szállítás",
                    Value = "8-10 munkanap"
                }
            };

            var variants = new List<AmindaVariant>();
            var values = new List<Value>();

            foreach(var value in matterhornProduct.Variants)
            {
                values.Add(
                    new Value()
                    {
                        Name = value.Name,
                    });
            }

            variants.Add(
                new AmindaVariant()
                {
                    Name = "Méret",
                    Values = new Values()
                    {
                        Value = values
                    }
                });

            var stocks = new List<Stock>();
            foreach(var stock in matterhornProduct.Variants)
            {
                stocks.Add(
                    new Stock()
                    {
                        Variants = new AmindaStockVariants()
                        {
                            AmindaStockVariant = stock.Name
                        },
                        Qty = stock.Stock
                    });
            }

            var brand = "MATTERHORN";
            AmindaProducts amindaProd = new()
            {
                Products = new List<Product>()
                {
                    new Product()
                    {
                        Action = "add",
                        Statuses = new Statuses()
                        {
                            Status = new Status()
                            {
                                Type = "base",
                                Value = 2
                            }
                        },
                        Export = new Export()
                        {
                            Status = 0
                        },
                        Sku = $"{brand.Substring(0,11-matterhornProduct.Id.ToString().Length)}{matterhornProduct.Id}",
                        Name = name,
                        Description = new Description()
                        {
                            Short =await Translate( matterhornProduct.Description)
                        },
                        Unit = "db",
                        Categories = new Categories()
                        {
                            Category = new Category()
                            {
                                Id = 678123,
                                Name = "Termékek|Alkategória 1",
                                Type = "base"
                            }
                        },
                        Prices = new Prices()
                        {
                            Price = new Price()
                            {
                                Type = "normal",
                                Gross = SalePriceCalculation(matterhornProduct.Prices["HUF"]),
                                Net = SalePriceCalculation(matterhornProduct.Prices["HUF"])
                            }
                        },
                        Images = new Images()
                        {
                            Image = images
                        },
                        Params = new Parameters()
                        {
                            Param = parameters
                        },
                        AmindaVariants = new AmindaVariants()
                        {
                            AmindaVariant = variants
                        },
                        Stocks = new Stocks()
                        {
                            Status= new Status()
                            {
                                Active = 1,
                                Empty = 0,
                                Variant = 1
                            },
                            Stock = stocks
                        }
                        
                        
                    }
                }
            };

            return amindaProd;            
        }

        private double SalePriceCalculation(double purchasePrice)
        {
            double salePrice;
            if (purchasePrice < ProductConstants.PriceLimitForDeliveryFee)
            {
                salePrice = (Math.Floor(purchasePrice * 2 / 1000)) * 1000 + 2990;
            } else
            {
                salePrice = (Math.Floor(purchasePrice * 2 / 1000)) * 1000 + 990;

            }
            return salePrice;
        }

        private async Task<string> Translate(string text)
        {
            Translation translation = null;
            using (DeepLClient client = new DeepLClient("8eec9c3d-166f-bf9e-c11a-7e5c433fc387:fx", useFreeApi: true))
            {
                try
                {
                    translation = await client.TranslateAsync(
                        text,
                        Language.Hungarian
                    );
                    Console.WriteLine(translation.DetectedSourceLanguage);
                    Console.WriteLine(translation.Text);
                }
                catch (DeepLException exception)
                {
                    Console.WriteLine($"An error occurred: {exception.Message}");
                }
            }
            return translation!=null ? translation.Text : "";
        }
    }
}
