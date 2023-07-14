using AmindaFeed.Constants;
using AmindaFeed.Extensions;
using AmindaFeed.Models;
using DeepL;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace AmindaFeed.Services
{
    public class MatterhornAdapter : IMatterhornAdapter
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public MatterhornAdapter(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<MatterhornProduct> GetMatterhornProduct(string productId)
        {
            MatterhornProduct matterhornProduct = new();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                $@"{_configuration.GetValue<string>("Matterhorn:BaseUrl")}/{productId}")
            {
                Headers =
                    {
                        { HeaderNames.Accept, "application/json" },
                        { HeaderNames.Authorization, _configuration.GetValue<string>("Matterhorn:ApiKey") }
                    }
            };

            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                matterhornProduct = await httpResponseMessage.Content.ReadFromJsonAsync<MatterhornProduct>() ??
                                    throw new NullReferenceException($"No matterhorn product with id {productId}");
            }
            return matterhornProduct;
        }

        public async Task<List<MatterhornProduct>> GetMatterhornProducts(List<string> productIds)
        {
            var matterhornProducts = new List<MatterhornProduct>();

            foreach (var productId in productIds)
            {
                matterhornProducts.Add(await GetMatterhornProduct(productId));
            }

            return matterhornProducts;
        }

        public async Task SetAmindaProductFromMatterhorn(string productId)
        {
            var product = await GetMatterhornProduct(productId);
            var amindaProd = await MatterhornAmindaMapper(product);

            var url = $"{_configuration.GetValue<string>("Unas:BaseUrl")}/setProduct";

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(url),
            };
            httpRequestMessage.Headers.Add("Accept", "application/xml");
            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await GetAmindaToken(_httpClient));
            httpRequestMessage.Content = new StringContent(amindaProd.ToXmlString(), Encoding.UTF8, "application/xml");

            Console.WriteLine($"aminda product xml : {amindaProd.ToXmlString()}");

            await _httpClient.SendAsync(httpRequestMessage)
                .ContinueWith(responseTask =>
                {
                    Console.WriteLine("Response AmindaProd : {0}", responseTask.Result);
                });
        }

        public void SetAmindaProductsFromMatterhorn(List<string> productIds)
        {
            productIds.ForEach(async productId =>
            {
                await SetAmindaProductFromMatterhorn(productId);
            });
        }

        private async Task<AmindaProducts> MatterhornAmindaMapper(MatterhornProduct matterhornProduct)
        {
            var name = matterhornProduct.Name.Replace(matterhornProduct.Id.ToString(), matterhornProduct.Color).Replace("model", "");
            var images = new List<Image>();
            var isImageFirst = true;
            var baseFolderPathToSave = _configuration.GetValue<string>("Matterhorn:PathToSaveConti");

            var pathToSave = baseFolderPathToSave + $"{matterhornProduct.Name.Trim()}";
            if (!Directory.Exists(pathToSave))
            {
                Directory.CreateDirectory(pathToSave);
            }
            SaveImageFromUrl(matterhornProduct.Images, pathToSave);

            foreach (var image in matterhornProduct.Images)
            {

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

            foreach (var value in matterhornProduct.Variants)
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
            foreach (var stock in matterhornProduct.Variants)
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

        private static double SalePriceCalculation(double purchasePrice)
        {
            double salePrice;
            if (purchasePrice < ProductConstants.PriceLimitForDeliveryFee)
            {
                salePrice = (Math.Floor(purchasePrice * 2 / 1000)) * 1000 + 2990;
            }
            else
            {
                salePrice = (Math.Floor(purchasePrice * 2 / 1000)) * 1000 + 990;

            }
            return salePrice;
        }

        private async Task<string> Translate(string text)
        {
            Translation translation = null;
            using (DeepLClient client = new DeepLClient(_configuration.GetValue<string>("DeepL:ApiKey"), useFreeApi: true))
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
            return translation != null ? translation.Text : "";
        }

        private async Task SaveImageFromUrl(List<string> urls, string savePath)
        {
            //using (var client = new HttpClient())
            //{
            foreach (string url in urls)
            {
                try
                {
                    // Get the filename from the URL and combine it with the save directory path
                    string filename = Path.GetFileName(url);
                    string filePath = Path.Combine(savePath, filename);

                    // Download the image and save it to disk
                    var response = await _httpClient.GetAsync(url);
                    using (var content = response.Content)
                    {
                        using (var stream = await content.ReadAsStreamAsync())
                        {
                            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                            {
                                await stream.CopyToAsync(fileStream);
                            }
                        }
                    }

                    Console.WriteLine("Saved image {0} to {1}", filename, filePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error saving image {0}: {1}", url, ex.Message);
                }
            }
            //}

            Console.WriteLine("All images saved!");
            Console.ReadLine();
        }

        private async Task<string> GetAmindaToken(HttpClient httpClient)
        {
            var login = new Params()
            {
                ApiKey = _configuration.GetValue<string>("Unas:ApiKey")
            };

            var settings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = true
            };


            XmlSerializer x = new XmlSerializer(login.GetType());
            StringBuilder output = new StringBuilder();
            var writer = new StringWriter(output);
            var xmlWriter = XmlWriter.Create(writer, settings);

            x.Serialize(xmlWriter, login);

            Console.WriteLine(output);

            var loginUrl = $"{_configuration.GetValue<string>("Unas:BaseUrl")}/login";



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

            return loginResponse.Token;
        }
    }
}
