﻿/*
 * imgUpload - jQuery Plugin
 * Copyright (c)感謝 2015 Knuckles Huang 
 */
(function ($) {
    $.fn.imgUpload = function (options) {
        options = $.extend({ //options 預設值
            action: 'imgur_upload_base64.php',
            multiple: true, //預設允許選取多張圖片
            maxWidth: 1024, //寬度預設限制最大1024px
            maxHeight: 0,   //高度預設無限制
            onSubmit: function () { },
            onComplete: function () { }
        }, options);

        var multiple = (options.multiple) ? 'multiple' : '';
        var btnWidth = this.width(),
			btnHeight = this.height();
        var input = $(
			'<input type="file" name="fileData[]" accept="image/*" ' + multiple + '/>'
		).css({
		    position: 'absolute', width: btnWidth, height: btnHeight,
		    opacity: 0, fontSize: 0, cursor: 'pointer'
		});
        var innertext = $(
			'<div>' + this.html() + '</div>'
		).css({
		    padding: '3px', textAlign: 'center', verticalAlign: 'middle'
		});
        this.empty().css({ padding: 0, textAlign: 'left' })
			.append(input).append(innertext);

        input.on('click', function () {
            this.value = null;
        }).on('change', function () {
            var files = this.files;
            //		console.log(files);
            var i, file;
            for (i = 0; i < files.length; i++) {
                file = files[i];
                if (!file.type.match(/image.*/)) {
                    console.log('file ' + (i + 1) + ' is not an image');
                    continue;
                }
                imgUpload(file, options);
            }
        });
        return this;
    };

    function imgUpload(file, options) {
        var type = file.type;
        var src = window.URL.createObjectURL(file);
        //隨機產生一個id，用來辨別不同的上傳檔案
        var id = Math.random().toString(36).substring(3, 7);

        options.onSubmit(id);

        var img = document.createElement("img");
        img.src = src;
        //	$('#selectImg').append(img);	
        img.onload = function () {
            var width = this.width,
				height = this.height,
				maxWidth = options.maxWidth,
				maxHeight = options.maxHeight;
            //		console.log('size:'+width+'x'+height);
            //寬或高大於設定的上限時，等比例縮小到符合上限
            if (width > height) {
                if (maxWidth > 0 && width > maxWidth) {
                    height *= maxWidth / width;
                    width = maxWidth;
                }
            } else {
                if (maxHeight > 0 && height > maxHeight) {
                    width *= maxHeight / height;
                    height = maxHeight;
                }
            }
            //使用縮小後的寬高建立一個canvas
            var canvas = document.createElement("canvas");
            canvas.width = width;
            canvas.height = height;
            var ctx = canvas.getContext("2d");
            ctx.drawImage(img, 0, 0, width, height);
            //將canvas轉為圖片的base64編碼
            var dataurl = canvas.toDataURL(type);
            //去掉 dataurl 開頭的 data:image/png;base64,
            var regex = new RegExp('^data:' + type + ';base64,');
            var base64 = dataurl.replace(regex, '');
            //將圖片的base64編碼上傳至網站，將回傳的JSON傳至onComplete

            $.post(options.action, { base64: base64 }, function (responseText) {
                if (!responseText.match(/^[\{\[]/)) { alert(responseText); return; }
                var responseJSON = JSON.parse(responseText);
                options.onComplete(responseJSON, id);
            }, 'text');


            var clientId = "11b1d561f60da1c";
            var imgUrl = base64;  //"http://i.imgur.com/l5OqYoZ.jpg"

            $.ajax({
                url: "https://api.imgur.com/3/upload",
                type: "POST",
                datatype: "json",
                data: {
                    image: imgUrl,
                    type: 'base64'
                },
                success: showMe,
                error: showMe,
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", "Client-ID " + clientId);
                }
            });

            function showMe(data) {
                //$("body").append(JSON.stringify(data));   //顯示全部資料
                if (data.success == true) {
                    // $('#picshow').prepend($('<img>', { id: 'theImg', src: data.data.link, align: 'middle', width: '150', height: '150' }))
                    var t = setTimeout("alert('圖片上傳中請稍後')", 2000);
                    $('#picshow').append("<img src='" + data.data.link + "'  align='middle'  style='max-height:500px'/>");
                    $('#upPic').val(data.data.link);
                    $('#upPic').append(JSON.stringify(data.data.link));
                    
                    
                    //$('#upPic').val(function (index, val) {
                    //    return val + data.data.link;
                    //});
                }
            }
        };
    }
})(jQuery);