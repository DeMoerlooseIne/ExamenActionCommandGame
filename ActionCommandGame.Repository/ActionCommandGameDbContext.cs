using ActionCommandGame.Model;
using ActionCommandGame.Repository.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ActionCommandGame.Repository;

public class ActionCommandGameDbContext : IdentityDbContext
{
    public ActionCommandGameDbContext(DbContextOptions<ActionCommandGameDbContext> options) : base(options)
    {
    }

    public DbSet<PositiveGameEvent> PositiveGameEvents { get; set; }
    public DbSet<NegativeGameEvent> NegativeGameEvents { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<PlayerItem> PlayerItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.RemovePluralizingTableNameConvention();
        modelBuilder.ConfigureRelationships();

        base.OnModelCreating(modelBuilder);
    }


    public void Initialize()
    {
        var emailTeacher = "bavo.ketels@vives.be";
        //Password Test123$
        var passwordHashTeacher =
            "AQAAAAEAACcQAAAAECp9VnV5jgDyqQqacxkrC+OcWFUM1+mavZ4+mxxhqtm/dg9UTVq1vhgAKFsblrEXDA==";

        var emailRick = "rick@hotmail.com";
        //Password Test123$
        var passwordHashRick = "AQAAAAEAACcQAAAAECp9VnV5jgDyqQqacxkrC+OcWFUM1+mavZ4+mxxhqtm/dg9UTVq1vhgAKFsblrEXDA==";

        var userAdmin = new IdentityUser
        {
            UserName = emailTeacher,
            Email = emailTeacher,
            NormalizedEmail = emailTeacher.ToUpperInvariant(),
            NormalizedUserName = emailTeacher.ToUpperInvariant(),
            PasswordHash = passwordHashTeacher
        };

        var user = new IdentityUser
        {
            UserName = emailRick,
            Email = emailRick,
            NormalizedEmail = emailRick.ToUpperInvariant(),
            NormalizedUserName = emailRick.ToUpperInvariant(),
            PasswordHash = passwordHashRick
        };

        SaveChanges();

        GeneratePositiveGameEvents();
        GenerateNegativeGameEvents();
        GenerateAttackItems();
        GenerateDefenseItems();
        GenerateFoodItems();
        GenerateDecorativeItems();

        //God Mode Item
        Items.Add(new Item
        {
            Name = "God Potion",
            Description = "This is almost how a GOD must feel.",
            Attack = 1000000,
            Defense = 1000000,
            Fuel = 1000000,
            ActionCooldownSeconds = 1,
            Price = 10000000
        });

        Players.Add(new Player
        { UserId = userAdmin.Id, Name = "Rick Sanchez", Money = 10000, ImageName = "Rick_Sanchez.png" });
        Players.Add(new Player
        {
            UserId = userAdmin.Id,
            Name = "Morty Smith",
            Money = 1000,
            Experience = 2000,
            ImageName = "Morty_Smith.png"
        });
        Players.Add(new Player
        {
            UserId = userAdmin.Id,
            Name = "Summer Smith",
            Money = 500,
            Experience = 5,
            ImageName = "Summer_Smith.png"
        });
        Players.Add(new Player
        {
            UserId = userAdmin.Id,
            Name = "Jerry Smith",
            Money = 1000000,
            Experience = 200,
            ImageName = "Jerry_Smith.png"
        });
        Players.Add(new Player
        {
            UserId = userAdmin.Id,
            Name = "Beth Smith",
            Money = 1000000,
            Experience = 200,
            ImageName = "Beth_Smith.png"
        });

        SaveChanges();
    }

    private void GeneratePositiveGameEvents()
    {
        PositiveGameEvents.Add(new PositiveGameEvent
        {
            Name = "The \"prime\" dimension",
            Description =
                "The primary universe where Rick and Morty live. This dimension is similar to our own, with a recognizable Earth-like planet and similar laws of physics.",
            Probability = 5
        });
        PositiveGameEvents.Add(new PositiveGameEvent
        {
            Name = "The \"C-137\" dimension",
            Description =
                "A parallel universe that is nearly identical to the prime dimension, but with some minor differences.",
            Probability = 10
        });
        PositiveGameEvents.Add(new PositiveGameEvent
        {
            Name = "The \"Fantasy Dimension,\"",
            Description =
                "A dimension that resembles a traditional fantasy world, complete with dragons, wizards, and other magical creatures.",
            Probability = 10
        });
        PositiveGameEvents.Add(new PositiveGameEvent
        {
            Name = "The \"Love Dimension,\"",
            Description =
                "A dimension that is ruled by a group of powerful, humanoid creatures who are obsessed with love and romance.",
            Probability = 1
        });
        PositiveGameEvents.Add(new PositiveGameEvent
        {
            Name = "The \"Boob World,\"",
            Description = "A dimension that is entirely inhabited by creatures with enormous, exaggerated breasts.",
            Probability = 2
        });
        PositiveGameEvents.Add(new PositiveGameEvent
        { Name = "You found another Portal Gun", Money = 1000, Experience = 3, Probability = 2000 });
        PositiveGameEvents.Add(new PositiveGameEvent
        { Name = "You found a MeeseeksBox", Money = 10000, Experience = 1, Probability = 300 });
        PositiveGameEvents.Add(new PositiveGameEvent
        { Name = "You found a Plumbus", Money = 1, Experience = 1, Probability = 300 });
        PositiveGameEvents.Add(new PositiveGameEvent
        {
            Name = "You found a Neutrino Bomb",
            Description =
                "This powerful weapon is capable of destroying entire planets, and is used by Rick on several occasions to eliminate threats to himself or his family.",
            Money = 10000,
            Experience = 20,
            Probability = 20
        });
        PositiveGameEvents.Add(new PositiveGameEvent
        { Name = "You found Gravity Boots", Money = 1000, Experience = 3, Probability = 1000 });
        PositiveGameEvents.Add(new PositiveGameEvent
        {
            Name = "You found a Mind Blower",
            Description =
                "This device allows the user to erase specific memories from their own or another person's mind. It is used by Rick to erase unwanted memories, or to manipulate the memories of others.",
            Money = 10,
            Experience = 25,
            Probability = 20
        });
        PositiveGameEvents.Add(new PositiveGameEvent
        {
            Name = "You found The Smith Family Clone Gun",
            Description =
                "This gun creates clones of the person it is pointed at, allowing Rick and Morty to have multiple copies of themselves for various purposes.",
            Money = 5000,
            Experience = 25,
            Probability = 20
        });
        PositiveGameEvents.Add(new PositiveGameEvent
        {
            Name = "You found a Time Crystal",
            Description =
                "This crystal has the ability to manipulate time, allowing the user to travel through different periods in history or to freeze time in a specific location. ",
            Money = 500,
            Experience = 25,
            Probability = 20
        });
        PositiveGameEvents.Add(new PositiveGameEvent
        { Name = "You found a Shrink Ray", Money = 100, Experience = 6, Probability = 700 });
        PositiveGameEvents.Add(new PositiveGameEvent
        {
            Name = "You found a Love Potion",
            Description =
                "This potion causes the person who drinks it to fall in love with the first person they see. ",
            Money = 20000,
            Experience = 25,
            Probability = 40
        });
        PositiveGameEvents.Add(new PositiveGameEvent
        { Name = "You found a Mind-Reading Glasses", Money = 100, Experience = 10, Probability = 500 });
        PositiveGameEvents.Add(new PositiveGameEvent
        {
            Name = "You found a Anomaly Eraser",
            Description =
                "This device allows the user to erase specific events or objects from the timeline, effectively changing the course of history.",
            Money = 5000,
            Experience = 35,
            Probability = 40
        });
        PositiveGameEvents.Add(new PositiveGameEvent
        {
            Name = "You found a Transmogrifier Gun",
            Description =
                " This gun allows the user to transform objects or living beings into different shapes or forms.",
            Money = 60,
            Experience = 15,
            Probability = 40
        });
        PositiveGameEvents.Add(new PositiveGameEvent
        {
            Name = "You found a Vindicators' Suit",
            Description =
                "These advanced suits of armor are worn by the Vindicators, a team of superheroes. The suits are equipped with various weapons and abilities, such as laser blasters and energy shields.",
            Money = 100,
            Experience = 40,
            Probability = 35
        });
        PositiveGameEvents.Add(new PositiveGameEvent
        {
            Name = "You found a Genetic Modifier",
            Description =
                "This device allows the user to alter the genetic makeup of living beings, giving them new traits or abilities.",
            Money = 3000,
            Experience = 50,
            Probability = 30
        });
    }

    public void GenerateNegativeGameEvents()
    {
        NegativeGameEvents.Add(new NegativeGameEvent
        {
            Name = "The \"Microverse,\"",
            Description =
                "A dimension that is much smaller than the prime dimension, and is inhabited by tiny, insect-like creatures, leaded by Zeep.",
            DefenseWithArmorDescription = "You can overcome your enemy Zeep",
            DefenseWithoutArmorDescription = "Zeep attacks you! You are hit! Auch! That hurts!",
            DefenseLoss = 2,
            Probability = 100
        });
        NegativeGameEvents.Add(new NegativeGameEvent
        {
            Name = "The \"Purge Dimension,\"",
            Description = "A dimension where all forms of violence and conflict are allowed and encouraged.",
            DefenseWithArmorDescription = "Killingspree!!! Murder everybody!!!",
            DefenseWithoutArmorDescription = "Oh No!! Let's get out of here before you get sliced!!",
            DefenseLoss = 50,
            Probability = 50
        });
        NegativeGameEvents.Add(new NegativeGameEvent
        {
            Name = "The \"Toxic Rick Dimension,\"",
            Description =
                "A dimension that is filled with toxic gas and is inhabited by versions of Rick Sanchez who have become mutated and extremely aggressive as a result of exposure to the gas.",
            DefenseWithArmorDescription = "Thanks to your armor you are safe from the toxicity.",
            DefenseWithoutArmorDescription =
                "You are covered with green slime! You are starting to turn toxic!! Let's get out of here!",
            DefenseLoss = 5,
            Probability = 100
        });
        NegativeGameEvents.Add(new NegativeGameEvent
        {
            Name = "Dimension 35-C",
            Description = "Dimension that has MegaTrees that contain MegaSeeds.",
            DefenseWithArmorDescription = "You are prepared this time, you have an invisible bag. You are safe.",
            DefenseWithoutArmorDescription =
                "You have to smuggle the MegaSeeds through inter-dimensional customs. Insert the MegaSeed!! IT HURTS SO BAD!!",
            DefenseLoss = 500,
            Probability = 50
        });
    }

    private void GenerateAttackItems()
    {
        Items.Add(new Item
        {
            Name = "Ray Gun",
            Attack = 50,
            Price = 50,
            ImageUrl = "https://static.wikia.nocookie.net/rickandmorty/images/9/98/Ray_gun.png"
        });
        Items.Add(new Item
        {
            Name = "Mind Blowers",
            Attack = 250,
            Price = 250,
            ImageUrl = "https://static.wikia.nocookie.net/rickandmorty/images/9/98/Rm.s03e08.s05.png"
        });
        Items.Add(new Item
        {
            Name = "Arsenal of Guns",
            Attack = 500,
            Price = 500,
            ImageUrl = "https://i.pinimg.com/originals/2a/36/da/2a36da3ecae3c154286bd33e12d3749b.jpg"
        });
        Items.Add(new Item
        {
            Name = "Laser Axe",
            Attack = 1500,
            Price = 1500,
            ImageUrl =
                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQeLAO4tY0yXN4dIPbfno3LTnQakyFJM4qFrg&usqp=CAU"
        });
        Items.Add(new Item
        {
            Name = "RobotArms with Weapons",
            Attack = 10000,
            Price = 10000,
            ImageUrl =
                "https://imagesvc.meredithcorp.io/v3/mm/image?q=60&c=sc&rect=4%2C24%2C1997%2C1021&poi=%5B1060%2C270%5D&w=2000&h=1000&url=https%3A%2F%2Fstatic.onecms.io%2Fwp-content%2Fuploads%2Fsites%2F6%2F2022%2F11%2F18%2FRick-and-Morty-season-6-111822-2.jpg"
        });
    }

    private void GenerateDefenseItems()
    {
        Items.Add(new Item
        {
            Name = "Purge Armor",
            Defense = 50,
            Price = 50,
            ImageUrl = "https://static.wikia.nocookie.net/rickandmorty/images/8/82/Suits_HQ.png"
        });
        Items.Add(new Item
        {
            Name = "Awesome Armor",
            Defense = 100,
            Price = 100,
            ImageUrl =
                "https://consequence.net/wp-content/uploads/2020/04/rick-and-morty-season-4-the-other-five-trailer-second-half-adult-swim.png"
        });
        Items.Add(new Item
        {
            Name = "Love Potion",
            Defense = 250,
            Price = 250,
            ImageUrl = "https://static.wikia.nocookie.net/rickandmorty/images/3/35/S1e6_love_potion.png"
        });
        Items.Add(new Item
        {
            Name = "Mister Meeseeks",
            Defense = 500,
            Price = 500,
            ImageUrl = "https://static.wikia.nocookie.net/rickandmorty/images/6/6c/MeeseeksHQ.png"
        });
        Items.Add(new Item
        {
            Name = "Neutrino Bomb",
            Defense = 1000,
            Price = 1000,
            ImageUrl =
                "https://static.wikia.nocookie.net/rickandmorty/images/c/c6/E8879297-B15A-4BF2-A246-AC19E057D482.jpeg"
        });
        Items.Add(new Item
        {
            Name = "Portal Gun",
            Defense = 10000,
            Price = 10000,
            ImageUrl = "https://static.wikia.nocookie.net/rickandmorty/images/5/55/Portal_gun.png"
        });
    }

    private void GenerateFoodItems()
    {
        Items.Add(new Item
        {
            Name = "Rick's Alcohol",
            ActionCooldownSeconds = 10,
            Fuel = 10,
            Price = 50,
            ImageUrl =
                "https://www.looper.com/img/gallery/what-does-rick-usually-drink-in-rick-and-morty/intro-1606344204.jpg"
        });
        Items.Add(new Item
        {
            Name = "Microverse Batery",
            ActionCooldownSeconds = 10,
            Fuel = 20,
            Price = 100,
            ImageUrl = "https://static.wikia.nocookie.net/rickandmorty/images/2/25/S2e6_Microverse_Battery.png"
        });
        Items.Add(new Item
        {
            Name = "IceCream",
            ActionCooldownSeconds = 10,
            Fuel = 60,
            Price = 300,
            ImageUrl =
                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRzOjp9zr4yKJmlvc6S3ns7lDOK3UlzcsI3AQ&usqp=CAU"
        });
        Items.Add(new Item
        {
            Name = "EyeHoles",
            ActionCooldownSeconds = 15,
            Fuel = 100,
            Price = 500,
            ImageUrl = "https://www.geekalerts.com/u/Rick-and-Morty-Eyehole-Chocolate-Truffles.jpg"
        });
        Items.Add(new Item
        {
            Name = "Pickle Rick",
            ActionCooldownSeconds = 20,
            Fuel = 100,
            Price = 1000,
            ImageUrl =
                "https://static.wikia.nocookie.net/rickandmorty/images/4/41/Pickle_rick_transparent_edgetrimmed.png"
        });
#if DEBUG
        //Items.Add(new Item { Name = "Developer Food", ActionCooldownSeconds = 1, Fuel = 1000, Price = 1 });
#endif
    }

    private void GenerateDecorativeItems()
    {
        Items.Add(new Item
        {
            Name = "Emmy Award",
            Description = "You just won an Emmy. Do you feel special now?",
            Price = 10,
            ImageUrl =
                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSKZJVDlxXxFRXfHZuIKubkobEsR0Go87_uyw&usqp=CAU"
        });
        Items.Add(new Item
        {
            Name = "Szechuan Sauce",
            Description = "One of Rick's ultimate dreams.",
            Price = 100000,
            ImageUrl =
                "https://www.tbsnews.net/sites/default/files/styles/big_2/public/images/2022/03/23/untitled-3.png"
        });
        Items.Add(new Item
        {
            Name = "Killer Body",
            Description = "Yes, show everyone how much money you are willing to spend on something useless!",
            Price = 500000,
            ImageUrl =
                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTCQrap2IbIeRh2lW8HycXhb7_-C9KUJG21vw&usqp=CAU"
        });
    }
}