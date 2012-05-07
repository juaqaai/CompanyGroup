(function () {
    var marker = '<!-- markdown -->';
    var converter = Markdown.getSanitizingConverter();
    var editor = new Markdown.Editor(converter, "", function () { alert("Do you need help?"); });

    editor.hooks.set("insertImageDialog", function (callback) {
        // see if there's an image selected that they intend on editing
        var wmd = $('#wmd-input');

        var editImage, content = wmd.selection ? wmd.selection.createRange().text : null;
        if (content) {
            // replace <img> with <editimg>, so we can easily use jquery to get the 'src' without it
            // being resolved by the browser (e.g. prevent '/foo.png' becoming 'http://localhost:12345/orchardlocal/foo.png').
            content = content.replace(/\<IMG/gi, "<editimg");
            var firstImg = $(content).filter("editimg");
            if (firstImg.length) {
                editImage = {
                    src: firstImg.attr("src"),
                    "class": firstImg.attr("class"),
                    style: firstImg.css("cssText"),
                    alt: firstImg.attr("alt"),
                    width: firstImg.attr("width"),
                    height: firstImg.attr("height"),
                    align: firstImg.attr("align")
                };
            }
        }
        wmd.trigger("orchard-admin-pickimage-open", {
            img: editImage,
            uploadMediaPath: wmd.data("mediapicker-uploadpath"),
            callback: function (data) {
                callback(data.img.src);
            }
        });
        return true;
    });

    editor.run();

    $('.grippie').TextAreaResizer();
})();
