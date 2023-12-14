mergeInto(LibraryManager.library, {

    InitYSDKExtern: function()
    {
        initSDK();
    },

    SaveDataExtern: function(date)
    {
        var dateString = UTF8ToString(date);
        var myobj = JSON.parse(dateString);
        player.setData(myobj);
    },

    LoadDataExtern: function()
    {
        player.getData().then(_date => {
            const myJSON = JSON.stringify(_date);
            myGameInstance.SendMessage('YandexReceiver', 'InvokeDataLoadEvent', myJSON);
        });
    },

    GetLanguageExtern: function () 
    {
        var lang = ysdk.environment.i18n.lang;
        var bufferSize = lengthBytesUTF8(lang) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(lang, buffer, bufferSize);

        return buffer;
    },

    ShowFullscreenAdvExtern : function () 
    {
        ysdk.adv.showFullscreenAdv({
            callbacks: {
                onClose: function(wasShown) {
                    myGameInstance.SendMessage('YandexReceiver', 'ContinueGame');
                    console.log ("adv close");
                },
                onOpen: function(open) {
                    myGameInstance.SendMessage('YandexReceiver', 'StopGame');
                    console.log ("adv open");
                },
                onError: function(error) {
                    myGameInstance.SendMessage('YandexReceiver', 'ContinueGame');
                    console.log ("adv error");
                }
            }
        })
    },

    ShowRewardedVideoExtern : function () 
    {
        ysdk.adv.showRewardedVideo({
            callbacks: {
                onOpen: () => {
                    myGameInstance.SendMessage('YandexReceiver', 'StopGame');
                    console.log('Video ad open.');
                },
                onRewarded: () => {
                    myGameInstance.SendMessage('YandexReceiver', 'OnRewarded');
                    console.log('Rewarded!');
                },
                onClose: () => {
                    myGameInstance.SendMessage('YandexReceiver', 'ContinueGame');
                    console.log('Video ad closed.');
                }, 
                onError: (e) => {
                    myGameInstance.SendMessage('YandexReceiver', 'ContinueGame');
                    console.log('Error while open video ad:', e);
                }
            }
        })
    },

});