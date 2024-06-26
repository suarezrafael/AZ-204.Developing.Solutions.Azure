﻿using AzureCacheRedis;
using Newtonsoft.Json;
using StackExchange.Redis;

string connectionString = "appcache1000.redis.cache.windows.net:6380,password=FGy8SbBmvXE4jRy0LwMzVAbSrSDKSSA6bAzCaDBRUFo=,ssl=True,abortConnect=False";

ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(connectionString);

//SetCacheData("u1", 10,100);
//SetCacheData("u1", 20, 200);
//SetCacheData("u1", 30, 300);
GetCacheData("u1");

void SetCacheData(string userId,int productId,int quantity)
{

    string key = String.Concat(userId, ":cartitems");
    IDatabase database=redis.GetDatabase();
    CartItem cartItem=new CartItem { ProductID = productId,Quantity=quantity };

    database.ListRightPush(key, JsonConvert.SerializeObject(cartItem));

    Console.WriteLine("Cache data set");
}

void GetCacheData(string userId)
{
    string key = String.Concat(userId, ":cartitems");
    IDatabase database = redis.GetDatabase();
    if (database.KeyExists(key))
    {
        long listLength = database.ListLength(key); 
        Console.WriteLine("The number of values are {0}",listLength);
        for(int i = 0;i < listLength;i++)
        {
            string value=database.ListGetByIndex(key,i);
            CartItem cartItem=JsonConvert.DeserializeObject<CartItem>(value);
            Console.WriteLine("Product ID {0}",cartItem.ProductID);
            Console.WriteLine("Quantity {0}", cartItem.Quantity);
        }
    }
    else
        Console.WriteLine("key does not exist");    
}