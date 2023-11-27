mergeInto(LibraryManager.library, {

    InitExtern: function(){
        initSDK();
    },

    ShowRewardedVideoExtern : function () 
    {
        ysdk.adv.showRewardedVideo({
            callbacks: {
                onOpen: () => {
                    myGameInstance.SendMessage('Yandex', 'PauseGame');
                    console.log('Video ad open.');
                },
                onRewarded: () => {
                    myGameInstance.SendMessage('Yandex', 'OnRewarded');
                    console.log('Rewarded!');
                },
                onClose: () => {
                    myGameInstance.SendMessage('Yandex', 'ContinueGame');
                    console.log('Video ad closed.');
                }, 
                onError: (e) => {
                    myGameInstance.SendMessage('Yandex', 'ContinueGame');
                    console.log('Error while open video ad:', e);
                }
            }
        })
    },

});