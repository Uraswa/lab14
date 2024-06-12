using System.Diagnostics.CodeAnalysis;
using l10;
using LAB12_4.AvlTreeNET;
using Laba10;

namespace lab14;

public class Program
{
    /// <summary>
    /// База данных игровых ключей. У Zeed два ключа активации.
    /// </summary>
    static ActivationKey[] _keysDatabase = new ActivationKey[]
    {
        new ActivationKey("Zeed", "IDJHH123HJS"),
        new ActivationKey("Zeed", "KJF123KAD23"),
        new ActivationKey("Minecraft", "ISO675KAD=="),
    };
    
    /// <summary>
    /// Структура для замены dynamic типа в задании с let
    /// </summary>
    public struct GameForSale
    {
        public Game Game;
        public string SaleEndDate;

    }
    private static Random _random;
    
    /// <summary>
    /// Главная функция приложения
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static void Main()
    {
        //с помощью функции хелпера получаем рандом, сид которого зависит
        //от текущего юникс времени
        _random = Helpers.Helpers.GetOrCreateRandom();

        while (true)
        {
            PrintMainMenu();
            uint option = Helpers.Helpers.EnterUInt("пункт меню", 1, 3);

            switch (option)
            {
                case 1:
                    LoopPart1();
                    break;
                case 2:
                    LoopPart2();
                    break;
                case 3:
                    return;
            }
            
            Console.Clear();
        }

    }

    /// <summary>
    /// Логика работы первой части
    /// </summary>
    [ExcludeFromCodeCoverage]
    private static void LoopPart1()
    {
        var collection = new SortedDictionary<int, List<Game>>();
        while (true)
        {
            PrintPart1Menu();
            uint option = Helpers.Helpers.EnterUInt("пункт меню", 1, 10);
            switch (option)
            {
                case 1: // сгенерировать коллекцию
                    uint storesCount = Helpers.Helpers.EnterUInt("количество магазинов", 1, 10);
                    collection = GetPart1Collection((int) storesCount);
                    Console.WriteLine("Коллекция успешно создана");
                    break;
                case 2: // добавить игру в коллекцию
                    uint storeIndex = Helpers.Helpers.EnterUInt("номер магазина", 1, (uint) collection.Count) - 1;
                    var game = GetUserGame();
                    collection[(int)storeIndex].Add(game);
                    break;
                case 3: // получить консольные игры
                    PrintResult("Консольные игры", GetConsoleGames(collection));
                    break;
                case 4: // получить VR игры
                    PrintResult("VR Игры", GetVRGames(collection));
                    break;
                case 5: // получить максимальное количество игроков в играх
                    if (collection.Count != 0)
                    {
                        var maxPlayers = GetMaxPlayersInGame(collection);
                        Console.WriteLine("Максимальное количество игроков в играх = " + maxPlayers);
                    } else 
                        Console.WriteLine("Коллекция пуста!");
                    break;
                case 6: // сгруппировать игры по типу устройства
                    if (collection.Count != 0)
                    {
                        foreach (var group in GroupGamesDevice(collection))
                        {
                            Console.WriteLine($"{group.Key}: количество элементов в группе: {group.Count()} ");
                            int index = 1;
                            foreach (var gm in group)
                            {
                                Console.WriteLine($"----Элемент #{index++}----");
                                gm.ShowVirtual();
                            }
                        }
                    } else 
                        Console.WriteLine("Коллекция пуста!");
                    break;
                case 7: // получить даты окончания распродаж игр
                    if (collection.Count != 0)
                    {
                        foreach (var gameForSale in GetGamesWithSaleEndDate(collection))
                        {
                            Console.WriteLine("Игра для распродажи:");
                            gameForSale.Game.ShowVirtual();
                            Console.WriteLine("Дата окончания распродажи: " + gameForSale.SaleEndDate);
                        }
                    } else 
                        Console.WriteLine("Коллекция пуста");
                    break;
                case 8: // join игры и их ключи активации
                    if (collection.Count != 0)
                    {
                        PrintResult("Игры с ключами активации ", GetGamesWithActivationKeys(collection, _keysDatabase));
                    } else 
                        Console.WriteLine("Коллекция пуста");
                    break;
                case 9: // вывести коллекцию
                    if (collection.Count == 0)
                    {
                        Console.WriteLine("Коллекция пуста");
                    }
                    else
                    {
                        foreach (var (storeId, games) in collection)
                        {
                            Console.WriteLine("Магазин #" + (storeId + 1));
                            foreach (var game1 in games)
                            {
                                game1.ShowVirtual();
                            }
                        }
                    }
                    break;
                case 10: // выход
                    return;
            }

            Console.WriteLine("Нажмите для продолжения...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    /// <summary>
    /// Логика работы 2-ой части
    /// </summary>
    [ExcludeFromCodeCoverage]
    private static void LoopPart2()
    {
        AvlTreeNet<Game> avlTree = new AvlTreeNet<Game>();
        while (true)
        {
            PrintMenuPart2();
            uint option = Helpers.Helpers.EnterUInt("пункт меню", 1, 8);
            
            switch (option)
            {
                case 1: // формирование дерева с помощью ДСЧ
                    uint size = Helpers.Helpers.EnterUInt("количество элементов в дереве", 1, 65536);
                    avlTree = FormAvlTree((int)size);
                    Console.WriteLine("Дерево успешно создано!");
                    break;
                case 2: // добавить игру в дерево
                    var game = GetUserGame();
                    if (avlTree.Contains(game))
                    {
                        Console.WriteLine("Элемент уже содержится в дереве!");
                    }
                    else
                    {
                        avlTree.Add(game);
                        Console.WriteLine("Элемент успешно добавлен");
                    }
                    break;
                case 3: //получить игры, в которые нужно играть в VR очках
                    if (avlTree.Count != 0) PrintResult(
                        "Игры, которые требуют vr очки", 
                        GetGamesThatRequireVrGlasses(avlTree)
                    );
                    else Console.WriteLine("Дерево пустое");
                    break;
                case 4: // Получить количество игр, в которые можно играть на консолях 
                    if (avlTree.Count != 0) 
                        Console.WriteLine("Количество игр, в которые можно играть на консолях: " + GetConsoleGamesCount(avlTree));
                    else 
                        Console.WriteLine("Дерево пустое");
                    break;
                case 5: // получить среднее количество игроков в играх
                    if (avlTree.Count != 0)
                    {
                        Console.WriteLine("Среднее количество игроков в играх: " + GetAveragePlayersInGamesCount(avlTree));

                    }
                    else
                    {
                        Console.WriteLine("Дерево пустое");
                    }
                    break;
                case 6: // группировка игр по типу
                    if (avlTree.Count != 0)
                    {
                        var result = GroupGamesByType(avlTree);
                        foreach (var group in result)
                        {
                            Console.WriteLine($"{group.Key}: количество элементов в группе: {group.Count()} ");
                            int index = 1;
                            foreach (var gm in group)
                            {
                                Console.WriteLine($"----Элемент #{index++}----");
                                gm.ShowVirtual();
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Дерево пустое");
                    }
                    break;
                case 7: // вывести дерево
                    avlTree.PrintTree();
                    break;
                case 8:
                    return;
            }
            
            Console.WriteLine("Нажмите для продолжения...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    /// <summary>
    /// Вывод поменю первой части
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static void PrintPart1Menu()
    {
        Console.WriteLine("1. Сгенерировать коллекцию с помощью ДСЧ");
        Console.WriteLine("2. Добавить игру в коллекцию");
        Console.WriteLine("3. Получить игры, в которые можно играть на консоли");
        Console.WriteLine("4. Получить VR игры");
        Console.WriteLine("5. Получить максимальное количество игроков в играх");
        Console.WriteLine("6. Сгруппировать видеоигры по типу устройства");
        Console.WriteLine("7. Получить игры и даты окончания распродажи");
        Console.WriteLine("8. Получить игры и их активационные ключи");
        Console.WriteLine("9. Вывести коллекцию");
        Console.WriteLine("10. Назад");
    }

    /// <summary>
    /// Вывод подменю 2 части
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static void PrintMenuPart2()
    {
        Console.WriteLine("1. Сформировать дерево с помощью ДСЧ");
        Console.WriteLine("2. Добавить элемент в дерево");
        Console.WriteLine("3. Получить игры, которые требует наличие vr очков");
        Console.WriteLine("4. Получить количество игр, в которые можно играть на консолях");
        Console.WriteLine("5. Получить среднее количество игроков в играх");
        Console.WriteLine("6. Сгруппировать игры по типу");
        Console.WriteLine("7. Вывести дерево");
        Console.WriteLine("8. Выход");
    }

    /// <summary>
    /// Вывод главного меню
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static void PrintMainMenu()
    {
        Console.WriteLine("Укажите часть задания:");
        Console.WriteLine("1. Часть 1");
        Console.WriteLine("2. Часть 2");
        Console.WriteLine("3. Выход");
    }

    /// <summary>
    /// Универсальная функция для вывода результата
    /// </summary>
    /// <param name="text">Сообщение</param>
    /// <param name="result">Коллекция для вывода</param>
    [ExcludeFromCodeCoverage]
    public static void PrintResult(string text, IEnumerable<object> result)
    {
        if (!result.Any())
        {
            Console.WriteLine("Ничего не найдено!");
            return;
        }
        
        Console.WriteLine(text);
        foreach (var obj in result)
        {
            Console.WriteLine(obj.ToString());
        }
    }

    #region QueriesPart1

    /// <summary>
    /// Получить игры, в которые можно играть с консоли
    /// </summary>
    /// <param name="collection">Коллекция игр по магазинам</param>
    /// <returns>Коллекцию игр, в которые можно играть на консоли (отложенное исполнение)</returns>
    public static IEnumerable<Game> GetConsoleGames(SortedDictionary<int, List<Game>> collection)
    {
        return from store in collection
            from game in store.Value
            where game is VideoGame && ((VideoGame)game).Device == Device.Console
            select game;
    }

    /// <summary>
    /// Получить все VR игры
    /// </summary>
    /// <param name="collection">Коллекция игр по магазинам</param>
    /// <returns>Коллекцию с VR играм (отложенное исполнение)</returns>
    public static IEnumerable<Game> GetVRGames(SortedDictionary<int, List<Game>> collection)
    {
        var videoGames = from store in collection
            from game in store.Value
            where game is VideoGame
            select game;
        var vrGames = from store in collection
            from game in store.Value
            where game is VRGame
            select game;
        return videoGames.Intersect(vrGames);
    }

    /// <summary>
    /// Получить максимальное количество игроков во всех играх
    /// </summary>
    /// <param name="collection">Коллекция игр по магазинам</param>
    /// <returns>Максимальное количество игроков во всех играх</returns>
    public static uint GetMaxPlayersInGame(SortedDictionary<int, List<Game>> collection)
    {
        return collection
            .SelectMany(pair => pair.Value)
            .Max(game => game.MaximumPlayers);
    }

    /// <summary>
    /// Сгруппировать игры по девайсам
    /// </summary>
    /// <param name="collection">Коллекция игр по магазинам</param>
    /// <returns>Игры, сгруппированные по девайсам (отложенное исполнение)</returns>
    public static IEnumerable<IGrouping<Device, Game>> GroupGamesDevice(SortedDictionary<int, List<Game>> collection)
    {
        return from store in collection
            from game in store.Value
            where game is VideoGame
            group game by ((VideoGame) game).Device;
    }

    /// <summary>
    /// Получить игры и к каждой игре добавить информацию об окончании скидок на нее
    /// </summary>
    /// <param name="collection">Коллекция игр по магазинам</param>
    /// <returns>Колецкцию игр,
    /// где у каждой игры есть дополнительно дата окончания скидки (отложенное исполнение)</returns>
    public static IEnumerable<GameForSale> GetGamesWithSaleEndDate(SortedDictionary<int, List<Game>> collection)
    {
        return (from store in collection
            from game in store.Value
            let saleEndDate = (game is VideoGame ? "01.07.2024" : "15.07.2024")
            select new
            {
                game, saleEndDate
            }).Select(d => new GameForSale(){Game = d.game, SaleEndDate = d.saleEndDate});
    }

    /// <summary>
    /// Запрос, получающий игру и ключ активации в игре, видеоигры без ключей актимации выбраны не будут
    /// </summary>
    /// <param name="collection">Игры по магазинам</param>
    /// <param name="keysDatabase">Ключи автивации</param>
    /// <returns>Коллекция dynamic с игрой и ключом (отложенное исполнение) </returns>
    public static IEnumerable<dynamic> GetGamesWithActivationKeys(SortedDictionary<int, List<Game>> collection, ActivationKey[] keysDatabase)
    {
        return from store in collection
                from game in store.Value
                where game is VideoGame
                join t in keysDatabase on (game.Name) equals t.GameName
                select new
                {
                    game, t
                };

    }
    
    /// <summary>
    /// Получаем коллекцию сети в магазинов и какие игры есть в каждом магазине.
    /// </summary>
    /// <param name="marketsCount"></param>
    /// <returns>Вернуть коллекцию магазинов с их ассортиментом</returns>
    public static SortedDictionary<int, List<Game>> GetPart1Collection(int marketsCount)
    {
        var markets = new SortedDictionary<int, List<Game>>();

        for (int i = 0; i < marketsCount; i++)
        {
            uint gamesCount = Helpers.Helpers.EnterUInt("количество игр в магазине #" + (i + 1).ToString(), 1, 5);
            markets.Add(i, GetGames((int)gamesCount));
        }

        return markets;

    }
    
    /// <summary>
    ///  Сгенерировать игры для магазина
    /// </summary>
    /// <param name="size">Количество игр в магазине</param>
    /// <returns>Коллекцию игр, которые находятся в магазине</returns>
    private static List<Game> GetGames(int size)
    {
        List<Game> games = new List<Game>();
        for (int i = 0; i < size; i++)
        {
            games.Add(GetRandomGame());
        }

        return games;
    }
    

    #endregion

    #region QueriesPart2

    /// <summary>
    /// Сформировать АВЛ дерево с помощью ДСЧ
    /// </summary>
    /// <param name="size"></param>
    /// <returns>Сформированное авл дерево</returns>
    public static AvlTreeNet<Game> FormAvlTree(int size)
    {
        var avlTree = new AvlTreeNet<Game>();
        for (int i = 0; i < size; i++)
        {
            avlTree.Add(GetRandomGame());
        }

        return avlTree;
    }

    /// <summary>
    /// Получить игры, в которые нужно играть с помощью VR очков
    /// </summary>
    /// <param name="tree">Коллекция из лаб12, в которой хранятся игры</param>
    /// <returns>Игры, в которые можно играть в вр очках</returns>
    public static IEnumerable<Game> GetGamesThatRequireVrGlasses(AvlTreeNet<Game> tree)
    {
        return tree.Where(g => g is VRGame vrGame && vrGame.AreVRGlassesRequired).Select(g => g);
    }

    /// <summary>
    /// Количество игр, в которые можно играть на консоли
    /// </summary>
    /// <param name="tree">Коллекция из лаб12, в которой хранятся игры</param>
    /// <returns>Количество игр, в которые можно играть на консоли</returns>
    public static int GetConsoleGamesCount(AvlTreeNet<Game> tree)
    {
        return tree.Count(g => g is VideoGame videoGame && videoGame.Device == Device.Console);
    }

    /// <summary>
    ///  Получить среднее количество игроков в играх
    /// </summary>
    /// <param name="tree">Коллекция из лаб12, в которой хранятся игры</param>
    /// <returns>Среднее количество игроков в играх</returns>
    public static double GetAveragePlayersInGamesCount(AvlTreeNet<Game> tree)
    {
        return tree.Average(g => g.MinimumPlayers);
    }

    /// <summary>
    /// Сгруппировать игры по их типу
    /// </summary>
    /// <param name="tree">Коллекция из лаб12, в которой хранятся игры</param>
    /// <returns>Сгруппированные типы по типу данных</returns>
    public static IEnumerable<IGrouping<string, Game>> GroupGamesByType(AvlTreeNet<Game> tree)
    {
        return from game in tree
                group game by game.GetGameType();
    }
    

    #endregion
    
    /// <summary>
    /// Создать рандомную игру с помощью ДСЧ
    /// </summary>
    /// <returns></returns>
    private static Game GetRandomGame()
    {
        var rnd = Helpers.Helpers.GetOrCreateRandom();

        var gameType = rnd.Next(1,4);
        Game game = null;
        switch (gameType)
        {
            case 1:
                game = new Game();
                break;
            case 2:
                game = new VideoGame();
                break;
            case 3:
                game = new VRGame();
                break;
        }

        game.RandomInit();
        return game;
    }
    
    /// <summary>
    /// Пользовательский ввод игры
    /// </summary>
    /// <returns>Корректная игра, введенная пользователем</returns>
    private static Game GetUserGame()
    {
        var gameType = Helpers.Helpers.EnterUInt("тип игры(1: игра, 2: видеоигра, 3: VR игра)", 1, 3);
        Game game = null;
        switch (gameType)
        {
            case 1:
                game = new Game();
                break;
            case 2:
                game = new VideoGame();
                break;
            case 3:
                game = new VRGame();
                break;
        }

        game.Init();
        return game;
    }

}

