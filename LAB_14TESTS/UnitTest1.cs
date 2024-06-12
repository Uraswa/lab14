using l10;
using lab14;
using Laba10;

namespace LAB_14TESTS;

public class Tests
{
    
    [Test]
    public void TestPart1CollectionFillEmpty()
    {
        var collection = Program.GetPart1Collection(0, null);
        Assert.IsTrue(collection.Count == 0);
    }
    
    [Test]
    public void TestPart1CollectionFill()
    {
        var collection = Program.GetPart1Collection(5, (i) => 2);
        Assert.IsTrue(collection.Count == 5);
        Assert.IsTrue((from keypair in collection
            from game in keypair.Value
            select game).Count() == 10);
    }

    #region First Query Test

    [Test]
    public void TestFirstRequestEmptyCollection()
    {
        var collection = new SortedDictionary<int, List<Game>>();
        Assert.IsTrue(Program.GetConsoleGames(collection).Count() == 0);
    }

    [Test]
    public void TestFirstRequestDifferentTypesOfGames()
    {
        var collection = new SortedDictionary<int, List<Game>>()
        {
            [0] = new List<Game>()
            {
                new Game("1", 1, 1, new Game.IdNumber(1)),
                new VideoGame("1", 1, 1, new Game.IdNumber(1), Device.Console, 1),
                new VRGame("1", 1, 1, new Game.IdNumber(1), Device.Console, 1, false, false)
            }
        };
        
        Assert.IsTrue(Program.GetConsoleGames(collection).Count() == 2);
        Assert.IsTrue(Program.GetConsoleGames(collection).ToArray()[0].Equals(collection[0][1]));
        Assert.IsTrue(Program.GetConsoleGames(collection).ToArray()[1].Equals(collection[0][2]));

    }
    
    #endregion
    
    #region Second Query Test

    [Test]
    public void TestSecondRequestEmptyCollection()
    {
        var collection = new SortedDictionary<int, List<Game>>();
        Assert.IsTrue(Program.GetVRGames(collection).Count() == 0);
    }

    [Test]
    public void TestSecondRequestDifferentTypesOfGames()
    {
        var collection = new SortedDictionary<int, List<Game>>()
        {
            [0] = new List<Game>()
            {
                new Game("1", 1, 1, new Game.IdNumber(1)),
                new VideoGame("1", 1, 1, new Game.IdNumber(1), Device.Console, 1),
                new VRGame("1", 1, 1, new Game.IdNumber(1), Device.Console, 1, false, false)
            }
        };
        
        Assert.IsTrue(Program.GetVRGames(collection).Count() == 1);
        Assert.IsTrue(Program.GetVRGames(collection).ToArray()[0].Equals(collection[0][2]));

    }
    
    #endregion
    
    #region Third Query Test

    [Test]
    public void TestThirdRequestEmptyCollection()
    {
        var collection = new SortedDictionary<int, List<Game>>();
        Assert.Catch<InvalidOperationException>(() => Program.GetMaxPlayersInGame(collection));
    }

    [Test]
    public void TestThirdRequestDifferentTypesOfGames()
    {
        var collection = new SortedDictionary<int, List<Game>>()
        {
            [0] = new List<Game>()
            {
                new Game("1", 1, 3, new Game.IdNumber(1)),
                new VideoGame("1", 1, 5, new Game.IdNumber(1), Device.Console, 1),
                new VRGame("1", 1, 4, new Game.IdNumber(1), Device.Console, 1, false, false)
            }
        };
        
        Assert.IsTrue(Program.GetMaxPlayersInGame(collection) == 5);

    }

    #endregion
    
    #region Fourth Query Test

    [Test]
    public void TestFourthRequestEmptyCollection()
    {
        var collection = new SortedDictionary<int, List<Game>>();
        Assert.IsTrue(Program.GroupGamesDevice(collection).Count() == 0);
    }

    [Test]
    public void TestFourthRequestDifferentTypesOfGames()
    {
        var collection = new SortedDictionary<int, List<Game>>()
        {
            [0] = new List<Game>()
            {
                new Game("1", 1, 1, new Game.IdNumber(1)),
                new VideoGame("1", 1, 1, new Game.IdNumber(1), Device.Mobile, 1),
                new VideoGame("1", 1, 3, new Game.IdNumber(1), Device.Mobile, 1),
                new VRGame("1", 1, 1, new Game.IdNumber(1), Device.Console, 1, false, false)
            }
        };

        var groups = Program.GroupGamesDevice(collection);
        Assert.IsTrue(groups.Count() == 2); 
        
        Assert.IsTrue(groups.ToArray()[0].Key == Device.Mobile 
                       && groups.ToArray()[0].ToArray()[0].Equals(collection[0][1])
                       && groups.ToArray()[0].ToArray()[1].Equals(collection[0][2]
        ));
        
        Assert.IsTrue(groups.ToArray()[1].Key == Device.Console
                      && groups.ToArray()[1].ToArray()[0].Equals(collection[0][3])
                      );
        
    }

    #endregion
    
    #region Fifth Query Test
    [Test]
    public void TestFifthRequestEmptyCollection()
    {
        var collection = new SortedDictionary<int, List<Game>>();
        Assert.IsTrue(Program.GetGamesWithSaleEndDate(collection).Count() == 0);
    }

    [Test]
    public void TestFifthRequestDifferentTypesOfGames()
    {
        var collection = new SortedDictionary<int, List<Game>>()
        {
            [0] = new List<Game>()
            {
                new Game("1", 1, 3, new Game.IdNumber(1)),
                new VideoGame("1", 1, 5, new Game.IdNumber(1), Device.Console, 1),
                new VRGame("1", 1, 4, new Game.IdNumber(1), Device.Console, 1, false, false)
            }
        };
        
        Assert.IsTrue(Program.GetGamesWithSaleEndDate(collection).Count() == 3);
        Assert.IsTrue(Program.GetGamesWithSaleEndDate(collection).ToArray()[0].SaleEndDate.Equals("15.07.2024"));
        Assert.IsTrue(Program.GetGamesWithSaleEndDate(collection).ToArray()[1].SaleEndDate.Equals("01.07.2024"));
        Assert.IsTrue(Program.GetGamesWithSaleEndDate(collection).ToArray()[2].SaleEndDate.Equals("01.07.2024"));

    }
    #endregion
    
    #region Sixth Query Test
    [Test]
    public void TestSixthRequestEmptyCollection()
    {
        var collection = new SortedDictionary<int, List<Game>>();
        Assert.IsTrue(Program.GetGamesWithActivationKeys(collection, Array.Empty<ActivationKey>()).Count() == 0);
    }

    [Test]
    public void TestSixthRequestEmptyActivationsKeys()
    {
        var collection = new SortedDictionary<int, List<Game>>()
        {
            [0] = new List<Game>()
            {
                new Game("1", 1, 3, new Game.IdNumber(1)),
                new VideoGame("1", 1, 5, new Game.IdNumber(1), Device.Console, 1),
                new VRGame("Zeed", 1, 4, new Game.IdNumber(1), Device.Mobile, 1, false, false)
            }
        };

        var keys = new ActivationKey[]
        {
            
        };

        var res = Program.GetGamesWithActivationKeys(collection, keys);
        
        Assert.IsTrue(res.Count() == 0);
    }
    
    [Test]
    public void TestSixthRequestNoJoin()
    {
        var collection = new SortedDictionary<int, List<Game>>()
        {
            [0] = new List<Game>()
            {
                new Game("1", 1, 3, new Game.IdNumber(1)),
                new VideoGame("1", 1, 5, new Game.IdNumber(1), Device.Console, 1),
                new VRGame("Zeed", 1, 4, new Game.IdNumber(1), Device.Mobile, 1, false, false)
            }
        };

        var keys = new ActivationKey[]
        {
            new ActivationKey("notExistGame", "test")
        };

        var res = Program.GetGamesWithActivationKeys(collection, keys);
        
        Assert.IsTrue(res.Count() == 0);
    }
    
    
    [Test]
    public void TestSixthRequestSingleJoin()
    {
        var collection = new SortedDictionary<int, List<Game>>()
        {
            [0] = new List<Game>()
            {
                new Game("1", 1, 3, new Game.IdNumber(1)),
                new VideoGame("1", 1, 5, new Game.IdNumber(1), Device.Console, 1),
                new VRGame("Zeed", 1, 4, new Game.IdNumber(1), Device.Mobile, 1, false, false)
            }
        };

        var keys = new ActivationKey[]
        {
            new ActivationKey("Zeed", "test")
        };

        var res = Program.GetGamesWithActivationKeys(collection, keys);
        
        Assert.IsTrue(res.Count() == 1);

        var item = res.ToArray()[0];
        var propertyInfoGame = item.GetType().GetProperty("game");
        var propertyInfoKey = item.GetType().GetProperty("key");
        Assert.IsTrue(((Game)propertyInfoGame.GetValue(item)).Equals(collection[0][2]));
        Assert.IsTrue(((ActivationKey)propertyInfoKey.GetValue(item)).Equals(keys[0]));
       
    }
    
    [Test]
    public void TestSixthRequestSingleToManyJoin()
    {
        var collection = new SortedDictionary<int, List<Game>>()
        {
            [0] = new List<Game>()
            {
                new Game("1", 1, 3, new Game.IdNumber(1)),
                new VideoGame("1", 1, 5, new Game.IdNumber(1), Device.Console, 1),
                new VRGame("Zeed", 1, 4, new Game.IdNumber(1), Device.Mobile, 1, false, false)
            }
        };

        var keys = new ActivationKey[]
        {
            new ActivationKey("Zeed", "test1"),
            new ActivationKey("Zeed", "test2"),
        };

        var res = Program.GetGamesWithActivationKeys(collection, keys);
        
        Assert.IsTrue(res.Count() == 2);

        for (int i = 0; i < keys.Length; i++)
        {
            var item = res.ToArray()[i];
            var propertyInfoGame = item.GetType().GetProperty("game");
            var propertyInfoKey = item.GetType().GetProperty("key");
            Assert.IsTrue(((Game)propertyInfoGame.GetValue(item)).Equals(collection[0][2]));
            Assert.IsTrue(((ActivationKey)propertyInfoKey.GetValue(item)).Equals(keys[i]));

        }
       
    }
    #endregion

    #region PART2

    [Test]
    public void TestAvlTreeFillEmpty()
    {
        var tree = Program.FormAvlTree(0);
        Assert.IsTrue(tree.Count == 0);
    }

    [Test]
    public void TestAvlTreeNotEmpty()
    {
        var tree = Program.FormAvlTree(3);
        Assert.IsTrue(tree.Count == 3);
    }

    [Test]
    public void TestRequest1EmptyTree()
    {
        var tree = Program.FormAvlTree(0);
        var res = Program.GetGamesThatRequireVrGlasses(tree);
        Assert.IsTrue(res.Count() == 0);
    }

    [Test]
    public void TestRequest1NotEmptyTree()
    {
        var tree = Program.FormAvlTree(0);
        tree.Add(new Game("1", 1, 1, new Game.IdNumber(1)));
        tree.Add(new VideoGame("1", 1, 1, new Game.IdNumber(1), Device.Console, 1));
        tree.Add(new VRGame("1", 1, 1, new Game.IdNumber(1), Device.Console, 1, false, true));
        var target = new VRGame("1", 1, 1, new Game.IdNumber(1), Device.Console, 1, true, true);
        tree.Add(target);
        var res = Program.GetGamesThatRequireVrGlasses(tree);
        Assert.IsTrue(res.Count() == 1);
        Assert.IsTrue(res.ToArray()[0].Equals(target));
    }
    
    [Test]
    public void TestRequest2EmptyTree()
    {
        var tree = Program.FormAvlTree(0);
        var res = Program.GetConsoleGamesCount(tree);
        Assert.IsTrue(res == 0);
    }
    
    [Test]
    public void TestRequest2NotEmptyTree()
    {
        var tree = Program.FormAvlTree(0);
        tree.Add(new Game("1", 1, 1, new Game.IdNumber(1)));
        var target1 = new VideoGame("1", 1, 1, new Game.IdNumber(1), Device.Console, 1);
        var target2 = new VRGame("1", 1, 1, new Game.IdNumber(1), Device.Console, 1, false, true);
        tree.Add(target1);
        tree.Add(target2);
        var res = Program.GetConsoleGamesCount(tree);
        Assert.IsTrue(res == 2);
    }
    
    [Test]
    public void TestRequest3EmptyTree()
    {
        var tree = Program.FormAvlTree(0);
        Assert.Catch<InvalidOperationException>(() =>Program.GetAveragePlayersInGamesCount(tree));
    }
    
    [Test]
    public void TestRequest3NotEmptyTree()
    {
        var tree = Program.FormAvlTree(0);
        tree.Add(new Game("1", 1, 1, new Game.IdNumber(1)));
        var target1 = new VideoGame("1", 1, 1, new Game.IdNumber(1), Device.Console, 1);
        var target2 = new VRGame("1", 1, 1, new Game.IdNumber(1), Device.Console, 1, false, true);
        tree.Add(target1);
        tree.Add(target2);
        var res = Program.GetAveragePlayersInGamesCount(tree);
        Assert.IsTrue(Math.Abs(res - 1) < 0.001);
    }
    
    [Test]
    public void TestRequest4EmptyTree()
    {
        var tree = Program.FormAvlTree(0);
        var res = Program.GroupGamesByType(tree);
        Assert.IsTrue(res.Count() == 0);
    }
    
    [Test]
    public void TestRequest4NotEmptyTree()
    {
        var tree = Program.FormAvlTree(0);
        var target0 = new Game("1", 1, 1, new Game.IdNumber(1));
        var target1 = new VideoGame("1", 1, 1, new Game.IdNumber(1), Device.Console, 1);
        var target2 = new VRGame("1", 1, 1, new Game.IdNumber(1), Device.Console, 1, false, true);
        tree.Add(target0);
        tree.Add(target1);
        tree.Add(target2);
        var res = Program.GroupGamesByType(tree);
        Assert.IsTrue(res.Count() == 3);
        Assert.IsTrue(res.ToArray()[2].Key == "Обычная игра");
        Assert.IsTrue(res.ToArray()[0].Key == "Видеоигра");
        Assert.IsTrue(res.ToArray()[1].Key == "VR Игра");
        
        Assert.IsTrue(res.ToArray()[2].ToArray()[0].Equals(target0));
        Assert.IsTrue(res.ToArray()[0].ToArray()[0].Equals(target1));
        Assert.IsTrue(res.ToArray()[1].ToArray()[0].Equals(target2));
    }

    #endregion

    [Test]
    public void TestActivationKeyToString()
    {
        var key = new ActivationKey("test", "123");
        Assert.AreEqual("Название игры=test; Ключ=123", key.ToString());
    }
}