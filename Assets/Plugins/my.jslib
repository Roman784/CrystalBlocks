mergeInto(LibraryManager.library, {

    InitYSDKExtern: function(){
        initSDK();
    },

    SaveDataExtern: function(date){
        var dateString = UTF8ToString(date);
        var myobj = JSON.parse(dateString);
        player.setData(myobj);
    },

    LoadDataExtern: function(){
        player.getData().then(_date => {
            const myJSON = JSON.stringify(_date);
            myGameInstance.SendMessage('YandexReceiver', 'InvokeDataLoadedEvent', myJSON);
        });
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