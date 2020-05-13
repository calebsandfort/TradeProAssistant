using Enums;

namespace TradeProAssistant.Data.Migrations
{
    using Contexts;
    using CsvHelper;
    using CsvHelper.Configuration;
    using CsvHelper.TypeConversion;
    using Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<TradeProAssistantContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TradeProAssistantContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            if (context.Securities.Count() == 0)
            {
                Assembly assembly = Assembly.GetExecutingAssembly();

                string resourceName = "TradeProAssistant.Data.SeedData.SP500_Constituents.csv";

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        CsvReader csvReader = new CsvReader(reader);
                        csvReader.Configuration.RegisterClassMap<SecurityMap>();
                        csvReader.Configuration.HeaderValidated = null;
                        csvReader.Configuration.MissingFieldFound = null;
                        var stocks = csvReader.GetRecords<Security>().ToArray();
                        context.Securities.AddOrUpdate(s => s.Symbol, stocks);
                    }
                }

                resourceName = "TradeProAssistant.Data.SeedData.SPMidCap_Constituents.csv";

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        CsvReader csvReader = new CsvReader(reader);
                        csvReader.Configuration.RegisterClassMap<SecurityMap>();
                        csvReader.Configuration.HeaderValidated = null;
                        csvReader.Configuration.MissingFieldFound = null;
                        var stocks = csvReader.GetRecords<Security>().ToArray();
                        context.Securities.AddOrUpdate(s => s.Symbol, stocks);
                    }
                }

                resourceName = "TradeProAssistant.Data.SeedData.SPSmallCap_Constituents.csv";

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        CsvReader csvReader = new CsvReader(reader);
                        csvReader.Configuration.RegisterClassMap<SecurityMap>();
                        csvReader.Configuration.HeaderValidated = null;
                        csvReader.Configuration.MissingFieldFound = null;
                        var stocks = csvReader.GetRecords<Security>().ToArray();
                        context.Securities.AddOrUpdate(s => s.Symbol, stocks);
                    }
                }

                resourceName = "TradeProAssistant.Data.SeedData.WeeklyOptionSecurities.csv";

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        CsvReader csvReader = new CsvReader(reader);
                        csvReader.Configuration.RegisterClassMap<SecurityMap>();
                        csvReader.Configuration.HeaderValidated = null;
                        csvReader.Configuration.MissingFieldFound = null;
                        var stocks = csvReader.GetRecords<Security>().ToArray();
                        //context.Securities.AddOrUpdate(s => s.Symbol, stocks);
                        foreach (Security stock in stocks)
                        {
                            Security s = context.Securities.FirstOrDefault(x => x.Symbol == stock.Symbol);
                            if (s == null)
                            {
                                context.Securities.Add(stock);
                            }
                            else
                            {
                                s.PairEligible = stock.PairEligible;
                                s.Sector = stock.Sector;
                                s.SectorEnum = stock.Sector.ToSector();
                                s.PairEligible = stock.PairEligible;
                            }
                        }
                        context.SaveChanges();
                    }
                }
            }

            AddFuturesContract(context, "E-mini S&P 500", "ES", .25m, 12.5m);
            AddFuturesContract(context, "Euro FX", "6E", .00005m, 6.25m);

            if (context.RiskParameters.Count() == 0)
            {
                context.RiskParameters.Add(new RiskParameters()
                {
                    Name = "Demo Month",
                    Active = true,
                    TpaDailyTarget = 150m,
                    TpaWeeklyTarget = 750m,
                    TpaMonthlyTarget = 3000m,
                    MyDailyTarget = 250m,
                    MyWeeklyTarget = 1250m,
                    MyMonthlyTarget = 5000m,
                    DailyStop = -150m,
                    WeeklyStop = -450m,
                    MonthlyStop = -1000m
                });
            }

            if (context.TradingSettings.Count() == 0)
            {
                context.TradingSettings.Add(new TradingSettings()
                {
                    MonthStart = new DateTime(2020, 5, 11),
                    RiskParametersIdentifier = context.RiskParameters.First().Identifier
                });
            }
        }

        private void AddFuturesContract(TradeProAssistantContext context, String name, String symbol, Decimal tickSize, Decimal tickValue)
        {
            if(!context.FutureContracts.Any(x => x.Symbol == symbol))
            {
                context.FutureContracts.Add(new FutureContract()
                {
                    Name = name,
                    Symbol = symbol,
                    TickSize = tickSize,
                    TickValue = tickValue
                });

                context.SaveChanges();
            }
        }

        private sealed class SecurityMap : ClassMap<Security>
        {
            public static Decimal ConvertStringToDecimal(String s)
            {
                if(String.IsNullOrEmpty(s))
                {
                    return 0m;
                }
                else
                {
                    return Decimal.Parse(s, NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint);
                }
            }

            public SecurityMap()
            {
                Map(x => x.Name);
                Map(x => x.Symbol);
                Map(x => x.MarketCap).ConvertUsing(row => ConvertStringToDecimal(row.GetField("MarketCap")));
                Map(x => x.Sector);
                Map(x => x.SectorEnum).ConvertUsing(row => row.GetField("Sector").ToSector());
                Map(x => x.AssetClass).ConvertUsing(row => row.GetField("Asset Class"));
                Map(x => x.AssetClassEnum).ConvertUsing(row => row.GetField("Asset Class").ToAssetClass());
                Map(x => x.PairEligible);
            }
        }

        private sealed class WeeklyOptionSecurityMap : ClassMap<Security>
        {
            public static Decimal ConvertStringToDecimal(String s)
            {
                if (String.IsNullOrEmpty(s))
                {
                    return 0m;
                }
                else
                {
                    return Decimal.Parse(s, NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint);
                }
            }

            public WeeklyOptionSecurityMap()
            {
                Map(x => x.Name);
                Map(x => x.Symbol);
                Map(x => x.MarketCap).ConvertUsing(row => ConvertStringToDecimal(row.GetField("MarketCap")));
                Map(x => x.Sector);
                Map(x => x.SectorEnum).ConvertUsing(row => row.GetField("Sector").ToSector());
                Map(x => x.AssetClass).ConvertUsing(row => row.GetField("Asset Class"));
                Map(x => x.AssetClassEnum).ConvertUsing(row => row.GetField("Asset Class").ToAssetClass());
                Map(x => x.PairEligible);
            }
        }
    }
}
