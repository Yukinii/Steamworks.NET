#### FOR THE ORIGINAL CODE GO TO [STEAMWORKS.NET](https://github.com/rlabrecque/Steamworks.NET)

#Task Async/Await Steamworks.NET Wrapper


##### Getting Started

```csharp
        private static SteamManager _Instance;
        public static SteamManager Instance => _Instance ?? new SteamManager();
        
        public SteamManager()
        {
            if (_Instance == null)
                _Instance = this;

            if (SteamAPI.RestartAppIfNecessary((AppId)432200))
                Environment.Exit(0);
            if (!SteamAPI.Init())
                Environment.Exit(0);
            
            SteamUGC.OnDownloadCompleted += DownloadCompleted;
            SteamUGC.OnItemInstalled += ItemInstalled;
        }
```
Notice the two events on the bottom? No more CallbackResult mess!

##### The Async/Await Part

Async/Await version:

```csharp
        private async void ItemInstalled(ItemInstalled item)
        {
             var result = await SteamUGC.GetItemDetailsAsync(item.PublishedFileId);
             Console.WriteLine("Installed: "+result.Details.Title);
        }
```

versus...

```csharp
        private void ItemInstalled(ItemInstalled_t param)
        {
            var call = SteamUGC.RequestUGCDetails(param.m_nPublishedFileId, 0);
            var detailRequestResult = new CallResult<SteamUGCRequestUGCDetailsResult_t>();
            detailRequestResult.Set(call, (result, isCached) =>
            {
                Console.WriteLine("Installed: " + result.m_details.m_rgchTitle);
            });
            //now hold the flow here somehow to return the data to a UI since the result will come in whenever..
        }
```

##### The Clean Part

No more '_t' class endings or other artifacts from automated code generation! 
Compare the following two screenshots to the two I took with this wrapper and you'll surely see why this wrapper is easier to use.

Steamworks.NET

<p align="center">
  <img src="http://img.prntscr.com/img?url=http://i.imgur.com/JDrHtfZ.png" width="350"/>
  <img src="http://img.prntscr.com/img?url=http://i.imgur.com/LXDlcsR.png" width="250"/>
</p>


Steamworks.NET Async/Await

<p align="center">
  <img src="http://img.prntscr.com/img?url=http://i.imgur.com/X05A8c2.png" width="350"/>
  <img src="http://img.prntscr.com/img?url=http://i.imgur.com/OOzvNev.png" width="250"/>
</p>

