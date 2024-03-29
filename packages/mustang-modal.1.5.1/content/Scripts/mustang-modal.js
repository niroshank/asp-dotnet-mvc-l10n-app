﻿var __isAsnyc = false,
    _title = '',
    _body = '',
    _buttons = [],
    _width = 0,
    _height = 0,
    _loadPath = '',
    _parameters = {},
    _callback = null,
    _animate = 'slideDown',
    _speed = 500,
    _escapeClose = false,
    _clickClose = false,
    _eq = null,
    _onClose = function() {},
    _onOpen = function() {},
    _allowAutoClose = 0,
    _showCloseButton = false,
    __isUseOpenMethod = false;


var _MustangHub = {

    definations: {
        activeModal: ".active-modal",
        mainMustangModal: ".mustang-modal",
        mustangModalTitle: ".mustang-modal-title",
        mustangModalBody: ".mustang-modal-body",
        mustangModalFooter: ".mustang-modal-footer",
        mustangModalBg: "#mustang-modal-bg",
        mustangModalContainer: ".mustang-modal-container",
        defaultMaxWidth: 800
    },

    loadMessageBox: function (options) {

        __isAsnyc = false;
        options = $.extend({

            title: '',
            body: '',
            width: 0,
            height: 0,
            buttons: [],
            animate: 'slideDown',
            speed: 500,
            escapeClose: false,
            clickClose: false,
            onClose: function () { },
            onOpen: function () { },
            allowAutoClose: 0,
            showCloseButton: false

        }, options);

        _title = options.title;
        _body = options.body;
        _width = options.width;
        _height = options.height;
        _buttons = options.buttons;
        _animate = options.animate;
        _speed = options.speed;
        _escapeClose = options.escapeClose;
        _clickClose = options.clickClose;
        _onClose = options.onClose;
        _onOpen = options.onOpen;
        _allowAutoClose = options.allowAutoClose;
        _showCloseButton = options.showCloseButton;
    },

    setTitle: function (title) {

        if (title == "") {
            return "";
        }

        return "<div class='mustang-modal-title'><h3>" + title + "</h3></div>";
    },

    setBody: function (body) {

        if (body == undefined || body == "") {
            return '<div class="mustang-modal-body"></div>';
        }

        return '<div class="mustang-modal-body">' + body + '</div>';
    },

    setWidth: function (width) {

        if (width == 0 || width == undefined) {
            return;
        }

        $(_MustangHub.definations.activeModal).css({

            'width': width,
            'margin-left': -(Math.floor(width / 2)) + 'px'
        });

        if (width > _MustangHub.definations.defaultMaxWidth) {
            $(_MustangHub.definations.activeModal).css({

                'max-width': width,
            });
        }
    },

    setHeight: function (height) {

        if (height == 0 || height == undefined) {

            height = "auto";
        } else {

            var mustangModalBody = $(_MustangHub.definations.activeModal + " " + _MustangHub.definations.mustangModalBody),
                paddingTop = mustangModalBody.css("padding-top").replace('px', ''),
                paddingBottom = mustangModalBody.css("padding-top").replace('px', '');

            height = Number(height) + Number(paddingBottom) + Number(paddingTop);
        }

        $(_MustangHub.definations.activeModal + " " + _MustangHub.definations.mustangModalBody).css({

            'height': (height)
        });
    },

    setButtons: function (buttons) {

        if (buttons == [] || buttons == "")
            return "";

        var buttonsHtml = '';

        for (var i = 0; i < buttons.length; i++) {

            var button = buttons[i];

            if (button.id == undefined) {
                button.id = _MustangHub.generateGuidId();
                buttons[i] = button;
            }

            if (button.style == undefined) {
                button.style = "default";
            }

            if (button.text == undefined) {
                button.text = "Button Name";
            }

            buttonsHtml += '<input id="' + button.id + '" type="button" class="button mustang-btn mustang-btn-' + button.style + ' pbutton" value="' + button.text + '"/>';
        }

        return '<div class="mustang-modal-footer mustang-modal-buttons">' + buttonsHtml + '</div>';
    },

    addBackground: function () {

        $('body').append('<div id="mustang-modal-bg"></div>');
        $('body').css('overflow', 'hidden');
    },

    removeBackground: function () {

        if ($(_MustangHub.definations.mainMustangModal).length == 0) {
            $("#mustang-modal-bg").remove();
            $('body').css('overflow', '');
        }
    },

    addModalContainer: function (modalHtml) {

        return "<div class='mustang-modal-container'>" + modalHtml + "</div>";
    },

    removeModalContainer: function () {

        $(_MustangHub.definations.activeModal).parent().remove();
    },

    appendModal: function (html) {

        //adding new modal
        if (_buttons.length == 0 || _showCloseButton == true) {

            $("body")
                .append(_MustangHub.addModalContainer('<div class="mustang-modal active-modal"><p class="mustang-modal-close" onclick="MustangModal.Close(); return false;">x</p>' + html + '</div>'));
        }
        else {
            $("body")
              .append(_MustangHub.addModalContainer('<div class="mustang-modal active-modal">' + html + '</div>'));
        }

        _MustangHub.resetActiveModal();

        //loading width and height properties
        _MustangHub.setWidth(_width);
        _MustangHub.setHeight(_height);

        //loading buttons callbacks
        var buttons = _buttons;

        if (_buttons != []) {

            $(".pbutton").on('click', function () {

                var parameterButtonId = $(this).attr('id');
                if (parameterButtonId == 'undefined') {
                    console.error("You must fill the id field(s).");
                    return false;
                }

                for (var t = 0; t < buttons.length; t++) {
                    if (buttons[t].id == parameterButtonId) {
                        $.call(this, buttons[t].callback);
                    }
                }

                return false;
            });
        }

        if (__isAsnyc) {
            ajaxMethods.load(_MustangHub.definations.activeModal + " " + _MustangHub.definations.mustangModalBody, _loadPath, _parameters, _callback);
        }

        if (_MustangHub.hasAnimate()) {

            var activeModal = $(_MustangHub.definations.activeModal),
                activeModalHeight = $(_MustangHub.definations.activeModal).height();

            activeModal.css({ top: '-' + activeModalHeight + 'px' });
        }

        if (_escapeClose == true) {

            _MustangHub.escapeClose();
        }

        if (_clickClose == true) {

            _MustangHub.clickClose();
        }
    },

    open: function () {

        var title = _MustangHub.setTitle(_title);
        var body = "";

        if (!__isAsnyc) {
            _MustangHub.setBody('');
            body = _MustangHub.setBody(_body);
        }
        else {
            body = _MustangHub.setBody();
        }

        var buttons = _MustangHub.setButtons(_buttons);
        $(_MustangHub.definations.activeModal).fadeIn(300);

        _MustangHub.appendModal(title + body + buttons);

        switch (_animate) {
            case "slideDown":
                $(_MustangHub.definations.activeModal)
                .animate({ top: '0px' }, _speed, function () {
                    _MustangHub.onOpen();
                    if (_allowAutoClose > 0) {
                        _MustangHub.autoClose(_allowAutoClose);
                    }
                });
                break;
            case "toggle":
                $(_MustangHub.definations.activeModal)
               .css({ top: "0", display: "none" })
               .slideDown(_speed, function () {
                   _MustangHub.onOpen();
                   if (_allowAutoClose > 0) {
                       _MustangHub.autoClose(_allowAutoClose);
                   }
               });
                break;
            case "fading":
                $(_MustangHub.definations.activeModal)
                .css({ top: "0", display: "none" })
                .fadeIn(_speed, function () {
                    _MustangHub.onOpen();
                    if (_allowAutoClose > 0) {
                        _MustangHub.autoClose(_allowAutoClose);
                    }
                });
                break;
            default:
        }
    },

    close: function () {

        var activeModal = $(_MustangHub.definations.activeModal),
            activeModalHeight = activeModal.height();

        if (_MustangHub.hasAnimate()) {

            switch (_animate) {
                case "slideDown":
                    $(_MustangHub.definations.activeModal)
                     .animate({ top: "-" + activeModalHeight + "px" }, _speed, function () {
                         _MustangHub.removeModalContainer();
                         $(activeModal).remove();
                         _MustangHub.onClose();
                         _MustangHub.resetActiveModal();
                         _MustangHub.resetOptions();
                     });
                    break;
                case "toggle":
                    $(_MustangHub.definations.activeModal)
                    .slideUp(_speed, function () {
                        _MustangHub.removeModalContainer();
                        $(activeModal).remove();
                        _MustangHub.onClose();
                        _MustangHub.resetActiveModal();
                        _MustangHub.resetOptions();
                    });
                    break;
                case "fading":
                    $(_MustangHub.definations.activeModal)
                    .fadeOut(_speed, function () {
                        _MustangHub.removeModalContainer();
                        $(activeModal).remove();
                        _MustangHub.onClose();
                        _MustangHub.resetActiveModal();
                        _MustangHub.resetOptions();
                    });
                    break;
                default:
            }

        } else {
            $(activeModal).remove();
            _MustangHub.resetActiveModal();
        }

        __isAsnyc = false;
    },

    resetActiveModal: function () {

        $(_MustangHub.definations.mainMustangModal).removeClass("active-modal");
        $(_MustangHub.definations.mainMustangModal).last().addClass("active-modal");

        $(_MustangHub.definations.mainMustangModal).css("z-index", 0);
        $(_MustangHub.definations.activeModal).css("z-index", 9999);

        if ($(_MustangHub.definations.mustangModalBg).length == 0) {
            _MustangHub.addBackground();
        }

        $(_MustangHub.definations.mustangModalBg).css("z-index", 9990);

        if ($(_MustangHub.definations.mainMustangModal).length == 0) {
            _MustangHub.removeBackground();
        }
    },

    generateGuidId: function () {

        var id = "";
        var value = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        for (var i = 0; i < 7; i++)
            id += value.charAt(Math.floor(Math.random() * value.length));

        return id;
    },

    hasAnimate: function () {

        return _animate != "" ? true : false;
    },

    escapeClose: function () {


        $(window).on("keyup", function (e) {

            if (e.keyCode == 27) {

                _MustangHub.close();
            }
        });
    },

    clickClose: function () {

        $(_MustangHub.definations.mustangModalContainer).on("click", function () {

            _MustangHub.close();
        });
    },

    onClose: function () {

        $.call(this, _onClose);
    },

    onOpen: function () {

        $.call(this, _onOpen);
    },

    resize: function () {

        _MustangHub.setWidth(_width);
        _MustangHub.setHeight(_height);
    },

    resetOptions: function () {

        _width = 0,
        _height = 0,
        _loadPath = '',
        _parameters = {},
        _callback = null,
        _animate = 'slideDown',
        _speed = 500,
        _escapeClose = false;
        _title = "";
        _body = "";
        _buttons = [];
        _clickClose = false;
        _onClose = function () { };
        _onOpen = function () { };
        _eq = null;
        _allowAutoClose = 0;
        _showCloseButton = false;
    },

    autoClose: function (second) {

        setTimeout(function () {

            MustangModal.close();
        }, second);
    }
};

var ajaxMethods = {

    load: function (element, url, parameters, callback) {

        $(element).load(url, parameters, callback);
    }
};

var MustangCrossInteraction = function () {

    this.load = function (url, parameters, callback) {

        if (_eq != null) {

            var element = $(_MustangHub.definations.mainMustangModal)
           .eq(_eq)
           .children(_MustangHub.definations.mustangModalBody);

            ajaxMethods.load(element, url, parameters, callback);

        }
    };

    this.iframe = function (url, width, height) {

        if (_eq != null) {

            if (height == 0 || height == undefined) {
                height = "100%";
            }

            if (width == 0 || width == undefined) {
                width = "100%";
            }

            _MustangHub.setBody('');
            var iframeHtml = '';

            iframeHtml += '<iframe style="width:' + width + ';height:' + height + ';" src="' + url + ' ">';
            iframeHtml += '</iframe>';

            _body = iframeHtml;

            $(_MustangHub.definations.mainMustangModal)
                .eq(_eq)
                .children(_MustangHub.definations.mustangModalBody)
                .html(iframeHtml);
            _MustangHub.resize();
        }
    };

    this.changeBody = function (html) {

        if (_eq != null) {

            $(_MustangHub.definations.mainMustangModal + " " + _MustangHub.definations.mustangModalBody)
                .eq(_eq)
                .remove();

            _body = _MustangHub.setBody(html);

            $(_MustangHub.definations.mainMustangModal + " " + _MustangHub.definations.mustangModalTitle)
                .eq(_eq)
                .after(_body);
        }
    };

    this.width = function (width) {

        if (_eq != null) {
            if (width == 0 || width == undefined) {
                return false;
            }

            $(_MustangHub.definations.mainMustangModal)
                .eq(_eq)
                .css({
                    'width': width,
                    'margin-left': -(Math.floor(width / 2)) + 'px'
                });


            if (width > _MustangHub.definations.defaultMaxWidth) {
                $(_MustangHub.definations.mainMustangModal)
                    .eq(_eq)
                    .css({
                        'max-width': width,
                    });
            }
        }
    };

    this.height = function (height) {

        if (_eq != null) {

            if (height == 0 || height == undefined) {
                return false;
            } else {

                var mustangModalBody = $(_MustangHub.definations.mainMustangModal)
                        .eq(_eq),
                    paddingTop = mustangModalBody.css("padding-top").replace('px', ''),
                    paddingBottom = mustangModalBody.css("padding-top").replace('px', '');

                height = Number(height) + Number(paddingBottom) + Number(paddingTop);
            }

            $(_MustangHub.definations.mainMustangModal + " " + _MustangHub.definations.mustangModalBody)
                .eq(_eq)
                .css({
                    'height': height
                });
        }
    };

    this.resetResize = function () {

        this.height();
        this.width();
    };
};

var MustangModal = {

    prop: function (options) {

        
        _MustangHub.loadMessageBox(options);
        return this;
    },

    load: function (loadPath, parameters, callback) {

        __isAsnyc = true;
        _MustangHub.setBody('');

        _parameters = parameters;
        _loadPath = loadPath;
        _callback = callback;

        return this;
    },

    openIframe: function (url, width, height) {

        if (height == 0 || height == undefined) {
            height = "100%";
        }

        if (width == 0 || width == undefined) {
            width = "100%";
        }

        _MustangHub.setBody('');
        var iframeHtml = '';

        iframeHtml += '<iframe style="width:' + width + ';height:' + height + ';" src="' + url + ' ">';
        iframeHtml += '</iframe>';

        _body = iframeHtml;

        return this;
    },

    open: function (selector) {

        if (selector != undefined) {

            _body = selector.html();
        }

        _MustangHub.open();
    },

    close: function () {

        _MustangHub.close();
    },

    changeBody: function (html) {

        $(_MustangHub.definations.activeModal + " " + _MustangHub.definations.mustangModalBody).remove();
        _body = _MustangHub.setBody(html);

        $(_MustangHub.definations.activeModal + " " + _MustangHub.definations.mustangModalTitle)
            .after(_body);

        _MustangHub.resize();
    },

    eq: function (eq) {

        _eq = eq;
        return new MustangCrossInteraction();
    },
};

MustangModal.Close = function () {

    _MustangHub.close();
};

//bind to modal for a and button elements
$(document).ready(function () {

    $("a.m-modal,button.m-modal").on("click", function () {

        var $this = $(this),
            title = $this.data("title"),
            type = $this.data("type"),
            target = $this.data("target"),
            speed = $this.data("speed"),
            animate = $this.data("animate"),
            width = $this.data("width"),
            height = $this.data("height"),
            escapeClose = $this.data("escapeclose"),
            clickClose = $this.data("clickclose");

        var prop = MustangModal.prop({
            title: title,
            animate: animate,
            speed: speed,
            width: width,
            height: height,
            escapeClose: escapeClose,
            clickClose: clickClose
        });

        switch (type) {
            case "iframe":
                prop.openIframe(target)
                    .open();
                break;
            case "load":
                prop.load(target)
                    .open();
                break;
            case "html":
                prop.open($(target));
                break;
            default:
        }
    });
});
