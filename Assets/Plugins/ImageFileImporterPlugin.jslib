var ImageFileImporterPlugin = {
  ImageFileImporterCaptureClick: function() {
    if (!document.getElementById('ImageFileImporter')) {
      var fileInput = document.createElement('input');
      fileInput.setAttribute('type', 'file');
      fileInput.setAttribute('id', 'ImageFileImporter');
      /*fileInput.setAttribute('accept', '.png, .jpg', .jpeg')*/
      fileInput.style.visibility = 'hidden';
      fileInput.onclick = function (event) {
        this.value = null;
      };
      fileInput.onchange = function (event) {
        /* This line calls a FileSelected() on the script attached to GameObject named "VRMLoader"
         * so you need to make sure the scene has a GameObject named "VRMLoader" and a script with a FileSelected() attached
         *
         * この行はシーン上の"VRMLoader"という名前のGameObjectに対して、ImageFileSelected()の呼び出しを行っています
         * なので"VRMLoader"という名前のGameObjectがシーン上にあるかを確認してください
        */
        SendMessage('VRMLoader', 'ImageFileSelected', URL.createObjectURL(event.target.files[0]));
      }
      document.body.appendChild(fileInput);
    }

    var OpenFileDialog = function() {
      document.getElementById('ImageFileImporter').click();
      document.getElementById('#canvas').removeEventListener('click', OpenFileDialog);
    };

    document.getElementById('#canvas').addEventListener('click', OpenFileDialog, false);
  }
};
mergeInto(LibraryManager.library, ImageFileImporterPlugin);