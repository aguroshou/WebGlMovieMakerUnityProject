var jsBridge = {
    execute: function(methodName, parameter) {
        // jsの文字列に変換する
        methodName = Pointer_stringify(methodName)
        parameter = Pointer_stringify(parameter)

        // 実行するメソッド名とパラメータをまとめる
        var jsonObj = {}
        jsonObj.methodName = methodName
        jsonObj.parameter = parameter

        var argsmentString = JSON.stringify(jsonObj)
        // カスタムイベントを作成して発行する
        var event = new CustomEvent('unityMessage', { detail: argsmentString })
        window.dispatchEvent(event)
    }
}

mergeInto(LibraryManager.library, jsBridge);