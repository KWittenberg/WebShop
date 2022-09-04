namespace WebShop.Data;

public class AppDbInitializer
{
    public static void Seed(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var db = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

            db.Database.EnsureCreated();


            // ProductCategory
            if (!db.ProductCategory.Any())
            {
                db.ProductCategory.AddRange(new List<ProductCategory>()
                {
                    new ProductCategory()
                    {
                        Title = "Book",
                        Description = "Book Category"
                    },
                    new ProductCategory()
                    {
                    Title = "Photo",
                    Description = "Photo Category"
                    }
                });
                db.SaveChanges();
            }

            // Product
            if (!db.Product.Any())
            {
                db.Product.AddRange(new List<Product>()
                    {
                        new Product()
                        {
                            ProductCategoryId = 1,
                            Title = "Groblja Požeske Doline",
                            ShortDescription = "Jedinstvena publikacija o grobljima Požeške doline, odnosno bivšeg kotara i općine Požega.",
                            Description =  "Jedinstvena publikacija o grobljima Požeške doline, odnosno bivšeg kotara i općine Požega, koja obrađuje tu tematiku na preko 160 stranica teksta i 60 kolor fotografija. Obzirom na razvedenost naselja - 207 i 159 groblja sa preko 700.000 m2 grobnih zemljišta, knjiga predstavlja do sada jedinu takovu publikaciju u Županiji, ali i šire.",
                            Author = "Tomislav Wittenberg",
                            Image = "/img/1996 Groblja Pozeske Doline/1996 Groblja Pozeske Doline.jpg",
                            Available = true,
                            Quantity = 70,
                            Price = 200,
                            Discount = false,
                            DiscountPrice = 150,
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now,
                            YearOfPublication = 1996,
                            Publisher = "Bolta",
                            Isbn = "978-953-9700-00-0",
                            BookCategory = BookCategory.Monography,
                            BookBinding = BookBinding.Soft,
                            NumberOfPages = 190,
                            Width = 170,
                            Height = 240,
                            Thickness = 10,
                            Weight = 350
                        },
                        new Product()
                        {
                            ProductCategoryId = 1,
                            Title = "Rudina",
                            ShortDescription = "Rudina je bila centar ili bolje rečeno središte centralne Slavonije u kojoj se spaja sve ono najbolje toga vremena.",
                            Description = "Rudina je bila centar ili bolje rečeno središte centralne Slavonije u kojoj se spaja sve ono najbolje toga vremena. 'Rudina se rudila od crvenih fresaka.' - Akademik Matko Peić. 'Ono što benediktinski samostan Rudinu i crkvu Sv. Mihovila čini izuzetnim u čitavoj Hrvatskoj zapravo je arhitektonska figuralna plastika.' - Gjuro Szabo 'Umjetnički izraz prisutan na ostacima bebediktinske opatije Rudina nosi određenu poruku vremena i prostora na tlu Hrvatske.' - Lidija Ivančević - Španiček, prof.",
                            Author = "Tomislav Wittenberg",
                            Image = "/img/1997 Rudina/1997 Rudina.jpg",
                            Available = true,
                            Quantity = 116,
                            Price = 300,
                            Discount = true,
                            DiscountPrice = 250,
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(30),
                            YearOfPublication = 1997,
                            Publisher = "Bolta",
                            Isbn = "978-953-9730-00-0",
                            BookCategory = BookCategory.Monography,
                            BookBinding = BookBinding.Hard,
                            NumberOfPages = 126,
                            Width = 223,
                            Height = 290,
                            Thickness = 16,
                            Weight = 845
                        },
                        new Product()
                        {
                            ProductCategoryId = 1,
                            Title = "Puvarija",
                            ShortDescription = "Prva cjelovita obrada sjeverno-zapadnog dijela Dilj-gore i naselja na diljskim padinama, od srednjeg vijeka do današnjih dana.",
                            Description = "Prva cjelovita obrada sjeverno-zapadnog dijela Dilj-gore i naselja na diljskim padinama, od srednjeg vijeka do današnjih dana.",
                            Author = "Tomislav Wittenberg",
                            Image = "/img/1998 Puvarija/1998 Puvarija.jpg",
                            Available = true,
                            Quantity = 100,
                            Price = 300,
                            Discount = true,
                            DiscountPrice = 250,
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(30),
                            YearOfPublication = 1998,
                            Publisher = "Bolta",
                            Isbn = "978-953-97322-1-2",
                            BookCategory = BookCategory.Monography,
                            BookBinding = BookBinding.Soft,
                            NumberOfPages = 314,
                            Width = 164,
                            Height = 234,
                            Thickness = 17,
                            Weight = 910
                        },
                        new Product()
                        {
                            ProductCategoryId = 1,
                            Title = "Sesvetacki Kraj U Srcu Poljadije",
                            ShortDescription = "Veliki je broj pjesnika, putopisaca i drugih putnika namjernika s mnogo laskavih riječi i superlativa opisalo ili opjevalo Požešku dolinu sve od Rimljana do današnjih dana.",
                            Description = "Veliki je broj pjesnika, putopisaca i drugih putnika namjernika s mnogo laskavih riječi i superlativa opisalo ili opjevalo Požešku dolinu sve od Rimljana do današnjih dana. Kao da ih je nešto tjeralo da svoje ushićenje i oduševljenje zapišu, kako sebi za sjećanje, a drugima kao pohvalu, što su bili u mogućnosti uživati u takvim prirodnim ljepotama. Putujući kočijom 24. lipnja 1782. godine Matija Piller i Ljudevit Mitterpacher (profesori Univerziteta u Budimpešti) iz Našica prema Požegi, dolaskom na cestovni prijevoj na Krndiji, zapisali su: 'Čim smo došli na vrh brda, odmah se ukazao novi prostor, opsegom manji od onog kojeg smo ostavili, ali ljepši, ljupkiji i raznolikiji.' A za požeški okoliš, koji je srednji vijek zvao 'Campuz Posseganus', zapisati će isti autori, da krasotom natkriljuje mnoge druge krajeve. Vjekoslav Klaić ističe 'Plodnost požeškoga polja, vinorodne lozne nasade, koje izmjenjuju šljivici i zlatoklasne njive. Između bilja osobito uspijeva duhan, koji je u XVIII. stoljeću bio daleko na glasu i otpreman u Italiju, Francusku, Nizozemsku i Švedsku.'",
                            Author = "Tomislav Wittenberg",
                            Image = "/img/2002 Sesvetacki Kraj U Srcu Poljadije/2002 Sesvetacki Kraj U Srcu Poljadije.jpg",
                            Available = true,
                            Quantity = 123,
                            Price = 300,
                            Discount = true,
                            DiscountPrice = 250,
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(30),
                            YearOfPublication = 2002,
                            Publisher = "Bolta",
                            Isbn = "978-953-97322-2-0",
                            BookCategory = BookCategory.Monography,
                            BookBinding = BookBinding.Soft,
                            NumberOfPages = 466,
                            Width = 170,
                            Height = 243,
                            Thickness = 22,
                            Weight = 805
                        },
                        new Product()
                        {
                            ProductCategoryId = 1,
                            Title = "Pjesme",
                            ShortDescription = "Mladi pjesnik Vladimir Hip realna je osoba koja razmišlja i osjeća bilo i ritam svijeta u kojem živi.",
                            Description = "Mladi pjesnik Vladimir Hip realna je osoba koja razmišlja i osjeća bilo i ritam svijeta u kojem živi. Zaokupljen stvarnošću, a pod utjecajem literature i literarnih trendova svojega doba, uz znanja koja akumulira u Gimnaziji, svoje refleksije i emocije pretače u pjesme, koje postaju pjesme svakog čovjeka, pjesme za jednu nedosanjanu mladost.",
                            Author = "Vladimir Hip",
                            Image = "/img/2004 Pjesme/2004 Pjesme.jpg",
                            Available = true,
                            Quantity = 24,
                            Price = 80,
                            Discount = true,
                            DiscountPrice = 50,
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(30),
                            YearOfPublication = 2004,
                            Publisher = "Bolta",
                            Isbn = "978-953-99531-0-0",
                            BookCategory = BookCategory.Poems,
                            BookBinding = BookBinding.Soft,
                            NumberOfPages = 40,
                            Width = 134,
                            Height = 188,
                            Thickness = 4,
                            Weight = 80
                        },
                        new Product()
                        {
                            ProductCategoryId = 1,
                            Title = "Putopisne Impresije",
                            ShortDescription = "Putopisne impresije Branka Živković su plod zajedničkog putovanja s Matkom Peić u Španjolsku i Pariz.",
                            Description = "Putopisne impresije Branka Živković su plod zajedničkog putovanja s Matkom Peić u Španjolsku i Pariz (1973. i 1975.) i bez Matka u Italiju (1974.), ali uvijek promišljajući, kako bi to vidio i doživio Matko. Svakako, da mu je Matko u Italiji mnogo nedostajao, ali je zato odabir tema, imajući u vidu grupno putovanje, omogućilo vlastito promišljanje.",
                            Author = "Branko Živković",
                            Image = "/img/2004 Putopisne Impresije/2004 Putopisne Impresije.jpg",
                            Available = true,
                            Quantity = 18,
                            Price = 100,
                            Discount = true,
                            DiscountPrice = 80,
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(30),
                            YearOfPublication = 2004,
                            Publisher = "Bolta",
                            Isbn = "978-953-99531-1-1",
                            BookCategory = BookCategory.Monography,
                            BookBinding = BookBinding.Soft,
                            NumberOfPages = 78,
                            Width = 154,
                            Height = 226,
                            Thickness = 5,
                            Weight = 145
                        },
                        new Product()
                        {
                            ProductCategoryId = 1,
                            Title = "Osam Generacija Thallera",
                            ShortDescription = "Može se slobodno istaći bez ikakvih preterivanja da je monografija OSAM GENERACIJA THALLERA ne samo izuzetna po formi i sadržaju, nego i jedinstvena po obradi doseljenih familija Požeštine.",
                            Description = "Može se slobodno istaći bez ikakvih preterivanja da je monografija OSAM GENERACIJA THALLERA ne samo izuzetna po formi i sadržaju, nego i jedinstvena po obradi doseljenih familija Požeštine. Prikupljajući građu i pišući o Nijemcima i Austrijancima u hrvatskom kulturnom krugu zaključio sam, da se o nekim obiteljima ne može pisati skraćeno u dvije tri rečenice, već zaslužuju navođenja svih bitnijih njihovih ostvarenja. Zato sam do sada posebno obrađivao dvije požeške obitelji: Scholla i Lehrmana. Redosljed sam odabirao prema raspoloživom materijalu i obljetnicama. Pomalo je otužno na kraju, kada se neke javljaju samo u određenom vremenskom razdoblju i nemaju potomaka. Svakako, da su i te obitelji ostavile duboki trag tom razdoblju, ali njih više nema u Požegi. Zato sam za ovu priliku obradio porodicu, za koju postoje zapisi od preko četiri stotine godina, a svaka je generacija, a i pojedinac, markantni je predstavnik svoga vremena. Tako sam se odlučio za osam generacija iza Pavla Thallera. Radi se o ne samo tako brojnoj, nego i dugovječnoj obitelji, a više njih su napose znameniti.",
                            Author = "Tomislav Wittenberg",
                            Image = "/img/2007 Osam Generacija Thallera/2007 Osam Generacija Thallera.jpg",
                            Available = true,
                            Quantity = 20,
                            Price = 200,
                            Discount = true,
                            DiscountPrice = 150,
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(30),
                            YearOfPublication = 2007,
                            Publisher = "Bolta",
                            Isbn = "978-953-99531-5-5",
                            BookCategory = BookCategory.Monography,
                            BookBinding = BookBinding.Soft,
                            NumberOfPages = 127,
                            Width = 200,
                            Height = 244,
                            Thickness = 10,
                            Weight = 500
                        },
                        new Product()
                        {
                            ProductCategoryId = 1,
                            Title = "Vetovo",
                            ShortDescription = "Stiješnjena između tri centra: Kutjeva, Kaptola i Jakšića, župa ili bolje rečeno područje župe Vetovo, već stoljećima odolijeva kao svojevrstan ‘kuglager’ u nastalom trenju, kako od silnica ovih centara, tako od pritiska sučeljavanja Papuka i Krndije te južne ravne plohe - plodne ravnice.",
                            Description = "Stiješnjena između tri centra: Kutjeva, Kaptola i Jakšića, župa ili bolje rečeno područje župe Vetovo, već stoljećima odolijeva kao svojevrstan ‘kuglager’ u nastalom trenju, kako od silnica ovih centara, tako od pritiska sučeljavanja Papuka i Krndije te južne ravne plohe - plodne ravnice. A kao pravo osvježenje su ruže vjetrova koje donose planinske mirise, ali i zapuhe vjetra i snijega, obilja izvora i voda s planina, kao i šarenilo boja polja. To, kao da i daje objašnjenje, kako je nastalo najtvrđe stijenje, hladni izvori voda - plodnost tla. Zato je bilo više razloga da opišem ovaj kraj - područje župe Vetovo.",
                            Author = "Tomislav Wittenberg",
                            Image = "/img/2008 Vetovo/2008 Vetovo.jpg",
                            Available = true,
                            Quantity = 75,
                            Price = 300,
                            Discount = true,
                            DiscountPrice = 250,
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(30),
                            YearOfPublication = 2008,
                            Publisher = "Bolta",
                            Isbn = "978-953-99531-7-9",
                            BookCategory = BookCategory.Monography,
                            BookBinding = BookBinding.Soft,
                            NumberOfPages = 308,
                            Width = 200,
                            Height = 244,
                            Thickness = 18,
                            Weight = 900
                        },
                        new Product()
                        {
                            ProductCategoryId = 1,
                            Title = "100 Godina DVD Buk",
                            ShortDescription = "Prvo osnovano vatrogasno društvo u Požeštini koje nije imalo sjedište u općinskom središtu je DVD Buk, osnovano daleke 1909. godine za naselja: Buk, Svilna, Resnik, Mihaljevci i Tulnik.",
                            Description = "Prvo osnovano vatrogasno društvo u Požeštini koje nije imalo sjedište u općinskom središtu je DVD Buk, osnovano daleke 1909. godine za naselja: Buk, Svilna, Resnik, Mihaljevci i Tulnik.",
                            Author = "Tomislav Wittenberg",
                            Image = "/img/2009 100 Godina DVD Buk/2009 100 Godina DVD Buk.jpg",
                            Available = true,
                            Quantity = 1,
                            Price = 150,
                            Discount = true,
                            DiscountPrice = 100,
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(30),
                            YearOfPublication = 2009,
                            Publisher = "Bolta",
                            Isbn = "978-953-99531-9-3",
                            BookCategory = BookCategory.Monography,
                            BookBinding = BookBinding.Soft,
                            NumberOfPages = 90,
                            Width = 203,
                            Height = 251,
                            Thickness = 6,
                            Weight = 285
                        },
                        new Product()
                        {
                            ProductCategoryId = 1,
                            Title = "Almanah Gimnazije",
                            ShortDescription = "Sam grad Požega imao je te 1948. godine 8.544 stanovnika pa je i priliv od preko jedne tisuće gimnazijalaca imao itekako odraza na sveukupan život u gradu. Promet uglavnom željeznicom ili konjskim zapregama.",
                            Description = "Sam grad Požega imao je te 1948. godine 8.544 stanovnika pa je i priliv od preko jedne tisuće gimnazijalaca imao itekako odraza na sveukupan život u gradu. Promet uglavnom željeznicom ili konjskim zapregama.",
                            Author = "Tomislav Wittenberg",
                            Image = "/img/2009 Almanah Gimnazije/2009 Almanah Gimnazije.jpg",
                            Available = true,
                            Quantity = 11,
                            Price = 200,
                            Discount = true,
                            DiscountPrice = 150,
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(30),
                            YearOfPublication = 2009,
                            Publisher = "Bolta",
                            Isbn = "978-953-99531-8-6",
                            BookCategory = BookCategory.Monography,
                            BookBinding = BookBinding.Soft,
                            NumberOfPages = 126,
                            Width = 195,
                            Height = 244,
                            Thickness = 8,
                            Weight = 445
                        },
                        new Product()
                        {
                            ProductCategoryId = 1,
                            Title = "Brestovac",
                            ShortDescription = "Sam grad Požega imao je te 1948. godine 8.544 stanovnika pa je i priliv od preko jedne tisuće gimnazijalaca imao itekako odraza na sveukupan život u gradu. Promet uglavnom željeznicom ili konjskim zapregama.",
                            Description = "Sam grad Požega imao je te 1948. godine 8.544 stanovnika pa je i priliv od preko jedne tisuće gimnazijalaca imao itekako odraza na sveukupan život u gradu. Promet uglavnom željeznicom ili konjskim zapregama.",
                            Author = "Tomislav Wittenberg",
                            Image = "/img/2011 Brestovac/2011 Brestovac.jpg",
                            Available = true,
                            Quantity = 22,
                            Price = 300,
                            Discount = true,
                            DiscountPrice = 250,
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(30),
                            YearOfPublication = 2011,
                            Publisher = "Bolta",
                            Isbn = "978-953-7846-00-8",
                            BookCategory = BookCategory.Monography,
                            BookBinding = BookBinding.Soft,
                            NumberOfPages = 160,
                            Width = 205,
                            Height = 257,
                            Thickness = 15,
                            Weight = 720
                        },
                        new Product()
                        {
                            ProductCategoryId = 1,
                            Title = "Smotra",
                            ShortDescription = "Velika mi je čast i zadovoljstvo što mi je povjereno uređivanje ovog broja koji je posvećen uglavnom Požetini. Odmah po protjerivanju Turaka osim vojničkih posada doseljavaju i Nijemci i Austrijanci.",
                            Description = "Velika mi je čast i zadovoljstvo što mi je povjereno uređivanje ovog broja koji je posvećen uglavnom Požetini. Odmah po protjerivanju Turaka osim vojničkih posada doseljavaju i Nijemci i Austrijanci. Obogatili su ovaj kraj s preko 2500 prezimena, naseljavali se u preko 140 naselja s preko 5000 stanovnika, znanjem obrta, uređenjem zemljišta, poljoprivrednim kulturama, graditeljstvom i dr.",
                            Author = "Tomislav Wittenberg",
                            Image = "/img/2012 Smotra/2012 Smotra.jpg",
                            Available = true,
                            Quantity = 99,
                            Price = 100,
                            Discount = true,
                            DiscountPrice = 70,
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(30),
                            YearOfPublication = 2012,
                            Publisher = "Bolta",
                            Isbn = "1330-6987",
                            BookCategory = BookCategory.Monography,
                            BookBinding = BookBinding.Soft,
                            NumberOfPages = 140,
                            Width = 164,
                            Height = 235,
                            Thickness = 9,
                            Weight = 275
                        },
                        new Product()
                        {
                            ProductCategoryId = 1,
                            Title = "Tekić",
                            ShortDescription = "U središnjem dijelu Požeštine, kojeg još Rimljani prozvaše 'Zlatnom dolinom – Valis aurea', sjeverno zapadno od sela Jakšića, smjestilo se selo Tekić.",
                            Description = "U središnjem dijelu Požeštine, kojeg još Rimljani prozvaše 'Zlatnom dolinom – Valis aurea', sjeverno zapadno od sela Jakšića, smjestilo se selo Tekić.",
                            Author = "Tomislav Wittenberg",
                            Image = "/img/2013 Tekic/2013 Tekic.jpg",
                            Available = true,
                            Quantity = 7,
                            Price = 300,
                            Discount = true,
                            DiscountPrice = 250,
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(30),
                            YearOfPublication = 2013,
                            Publisher = "Bolta",
                            Isbn = "978-953-7846-01-5",
                            BookCategory = BookCategory.Monography,
                            BookBinding = BookBinding.Hard,
                            NumberOfPages = 104,
                            Width = 206,
                            Height = 257,
                            Thickness = 12,
                            Weight = 520
                        },
                        });
                db.SaveChanges();
            }
            
            // Hero
            if (!db.Hero.Any())
            {
                db.Hero.AddRange(new List<Hero>()
                {
                    new Hero()
                    {
                        Publish = true,
                        ImageUrl = "/img/hero/01-rudina.jpg",
                        Title = "Rudina",
                        Subtitle = "T. Wittenberg",
                        Description = "Rudina je bila centar ili bolje rečeno središte centralne Slavonije u kojoj se spaja sve ono najbolje toga vremena. 'Rudina se rudila od crvenih fresaka.' - Akademik Matko Peić.",
                        ProductUrl = "/Shop/ItemView/2",
                        Align = "col-lg-5",
                        ColorTitle = "#000000",
                        ColorDescription = "#000000",
                        EventName = "Sale",
                        EventText = "- 17%"
                    },
                    new Hero()
                    {
                    Publish = true,
                    ImageUrl = "/img/hero/02-ag.jpg",
                    Title = "Almanah Gimnazije '48",
                    Subtitle = "T. Wittenberg",
                    Description = "Iako se pišu monografije o samo onim osobama koje su, kako se to obično kaže ‘postigle’ nešto o životu, a onda svakako da su završile gimnaziju, ja sam se opredijelio da navedem sve učenike upisnike te školske godine 1948./49.",
                    ProductUrl = "/Shop/ItemView/10",
                    Align = "col-lg-5 offset-sm-7",
                    ColorTitle = "#b3b3b3",
                    ColorDescription = "#ffffff",
                    EventName = null,
                    EventText = null
                    },
                    new Hero()
                    {
                    Publish = true,
                    ImageUrl = "/img/hero/03-puvarija.jpg",
                    Title = "Puvarija",
                    Subtitle = "T. Wittenberg",
                    Description = "Prva cjelovita obrada sjeverno-zapadnog dijela Dilj-gore i naselja na diljskim padinama, od srednjeg vijeka do današnjih dana.",
                    ProductUrl = "/Shop/ItemView/3",
                    Align = "col-lg-5",
                    ColorTitle = "#000000",
                    ColorDescription = "#000000",
                    EventName = null,
                    EventText = null
                    },
                    new Hero()
                    {
                    Publish = true,
                    ImageUrl = "/img/hero/04-vetovo.jpg",
                    Title = "Vetovo",
                    Subtitle = "T. Wittenberg",
                    Description = "Stiješnjena između tri centra: Kutjeva, Kaptola i Jakšića, župa ili bolje rečeno područje župe Vetovo, već stoljećima odolijeva kao svojevrstan ‘kuglager’ u nastalom trenju.",
                    ProductUrl = "/Shop/ItemView/8",
                    Align = "col-lg-5 offset-sm-7",
                    ColorTitle = "#b3b3b3",
                    ColorDescription = "#ffffff",
                    EventName = "Sale",
                    EventText = "- 17%"
                    }
                });
                db.SaveChanges();
            }
        }
    }
}