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

            if (!context.RiskParameters.Any(x => x.Name == "7/11 Micro"))
            {
                context.RiskParameters.Add(new RiskParameters()
                {
                    Name = "7/11 Micro",
                    Active = true,
                    TpaDailyTarget = 21m,
                    TpaWeeklyTarget = 21m * 5m,
                    TpaMonthlyTarget = 21m * 5m * 4m,
                    MyDailyTarget = 0,
                    MyWeeklyTarget = 0,
                    MyMonthlyTarget = 0,
                    DailyStop = -21m,
                    WeeklyStop = -21m * 3m,
                    MonthlyStop = (21m * 5m * 4m) / -3m
                });
            }

            if (!context.RiskParameters.Any(x => x.Name == "6/10 Micro"))
            {
                Decimal profitBase = 10m * 1.25m;
                Decimal lossBase = 6m * 1.25m;

                context.RiskParameters.Add(new RiskParameters()
                {
                    Name = "6/10 Micro",
                    Active = true,
                    TpaDailyTarget = profitBase * 1.5m,
                    TpaWeeklyTarget = profitBase * 1.5m * 5m,
                    TpaMonthlyTarget = profitBase * 1.5m * 5m * 2m,
                    MyDailyTarget = 0,
                    MyWeeklyTarget = 0,
                    MyMonthlyTarget = 0,
                    DailyStop = -lossBase * 2.5m,
                    WeeklyStop = -lossBase * 2.5m * 2.5m,
                    MonthlyStop = -75m
                });
            }

            if (!context.RiskParameters.Any(x => x.Name == "2/6/8 Micro"))
            {
                Decimal profitBase = 2m * 8m * 1.25m;
                Decimal lossBase = 2m * 6m * 1.25m;

                context.RiskParameters.Add(new RiskParameters()
                {
                    Name = "2/6/8 Micro Micro",
                    Active = true,
                    TpaDailyTarget = profitBase * 1.5m,
                    TpaWeeklyTarget = profitBase * 1.5m * 5m,
                    TpaMonthlyTarget = profitBase * 1.5m * 5m * 4m,
                    MyDailyTarget = 0,
                    MyWeeklyTarget = 0,
                    MyMonthlyTarget = 0,
                    DailyStop = -lossBase * 2m,
                    WeeklyStop = -lossBase * 2m * 2.5m,
                    MonthlyStop = -lossBase * 2m * 2.5m * 2.5m
                });
            }

            if (!context.RiskParameters.Any(x => x.Name == "2/5/8 Micro"))
            {
                Decimal profitBase = 2m * 8m * 1.25m;
                Decimal lossBase = 2m * 5m * 1.25m;

                context.RiskParameters.Add(new RiskParameters()
                {
                    Name = "2/5/8 Micro",
                    Active = true,
                    TpaDailyTarget = profitBase * 1.75m,
                    TpaWeeklyTarget = profitBase * 1.75m * 5m,
                    TpaMonthlyTarget = profitBase * 1.75m * 5m * 4m,
                    MyDailyTarget = 0,
                    MyWeeklyTarget = 0,
                    MyMonthlyTarget = 0,
                    DailyStop = -lossBase * 2.5m,
                    WeeklyStop = -lossBase * 2.5m * 2.5m,
                    MonthlyStop = -lossBase * 2.5m * 2.5m * 2.5m
                });
            }

            if (!context.RiskParameters.Any(x => x.Name == "1 Lot Micro"))
            {
                context.RiskParameters.Add(new RiskParameters()
                {
                    Name = "1 Lot Micro",
                    Active = true,
                    TpaDailyTarget = 15m,
                    TpaWeeklyTarget = 75m,
                    TpaMonthlyTarget = 300m,
                    MyDailyTarget = 25m,
                    MyWeeklyTarget = 125m,
                    MyMonthlyTarget = 500m,
                    DailyStop = -15m,
                    WeeklyStop = -45m,
                    MonthlyStop = -100m
                });
            }

            if (!context.RiskParameters.Any(x => x.Name == "1.5 Lot Micro"))
            {
                Decimal accountSize = 450m;

                context.RiskParameters.Add(new RiskParameters()
                {
                    Name = "1.5 Lot Micro",
                    Active = true,
                    TpaDailyTarget = accountSize * .05m,
                    TpaWeeklyTarget = accountSize * .05m * 5m,
                    TpaMonthlyTarget = accountSize * .05m * 5m * 4m,
                    MyDailyTarget = 25m,
                    MyWeeklyTarget = 125m,
                    MyMonthlyTarget = 500m,
                    DailyStop = -accountSize * .05m,
                    WeeklyStop = -accountSize * .05m * 2.5m,
                    MonthlyStop = -accountSize * .4m
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
