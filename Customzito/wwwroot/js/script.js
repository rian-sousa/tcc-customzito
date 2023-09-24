$(document).ready(function () {

    Dropzone.options.dzCustom = { // camelized version of the `id`
        paramName: "file", // The name that will be used to transfer the file
        maxFilesize: 50, // MB
        autoProcessQueue: true,
        accept: function (file, done) {
            if (file.name == "justinbieber.jpg") {
                done("Naha, you don't.");
            }
            else { done(); }
        }
    };

})