
//(function ($) {

//	$.sideBarImpl = {
//        isInit : false,
//	    isIndentOpen: false,
//		isResourcesOpen: false,
//		isConfiguratiosOpen: false,
//		iconSize: 80,

//		init: function () {
//		    $("div.sidebarLiveDisplay").hide();

//			$("#sidebarIndent").hide();
//			$("#sidebarResources").hide();
//			$("#sidebarConfig").hide();
			
//			$("#indentLink").on('click', function (e) {
//			    $.sideBarImpl.handleIndentClick();
			    
//			});

//			$("#resourceLink").on('click', function (e) {
//			    $.sideBarImpl.handleResourceClick();
			    
//			});

//			$("#configLink").on('click', function (e) {
//			    $.sideBarImpl.handleConfigClick();
			    
//			});

//			$("div.sideBar").on('click', function (e) {
//				if ($.sideBar.get_isOpen()) {
//				    e.stopPropagation();
//				}
//			});
			
//			$(document).click(function (e) {
//				if ($.sideBar.get_isOpen()) {
//					$.sideBarImpl.closeAll();
//				}
//			});
//		},

//		handleIndentClick: function () {
		    
//		    //$.sideBarImpl.isIndentOpen = false;
//		    $.sideBarImpl.isResourcesOpen = false;
//		    $.sideBarImpl.isConfiguratiosOpen = false;
//		    $.sideBarImpl.isReportsOpen = false;
//		    //$("#sidebarIndent").hide();
//		    $("#sidebarResources").hide();
//		    $("#sidebarConfig").hide();
//		    $("#sidebarReports").hide();

//		    if (!$.sideBarImpl.isIndentOpen) {
//		        $("#sidebarIndent").show();
//		        var NewElementWidth = 300;//$("#sidebarIndent img").width();
//		        var animateWidthTo = NewElementWidth + $.sideBarImpl.iconSize;
//		        $("div.sideBar").animate({ width: animateWidthTo, duration: 600 });
//		        $.sideBarImpl.isIndentOpen = !$.sideBarImpl.isIndentOpen;
//		    }
//		    else {
//		        $("div.sideBar").animate({ width: $.sideBarImpl.iconSize, duration: 600 }, function () {
//		            $("#sidebarIndent").hide();
//		        });
//		        $.sideBarImpl.isIndentOpen = !$.sideBarImpl.isIndentOpen;
//		    }
//		    if ($.sideBarImpl.isIndentOpen) $("div.sidebarLiveDisplay").show();
//		    else $("div.sidebarLiveDisplay").hide();
//		    return false;
		    
//		},

//		handleResourceClick: function () {
		    
//		        $.sideBarImpl.isIndentOpen = false;
//		        //$.sideBarImpl.isResourcesOpen = false;
//		        $.sideBarImpl.isConfiguratiosOpen = false;
//		        $.sideBarImpl.isReportsOpen = false;
//		        $("#sidebarIndent").hide();
//		        //$("#sidebarResources").hide();
//		        $("#sidebarConfig").hide();
//		        $("#sidebarReports").hide();

//		        if (!$.sideBarImpl.isResourcesOpen) {
//		            $("#sidebarResources").show();
//		            var NewElementWidth = 300;//$("#sidebarResources img").width();
//		            var animateWidthTo = NewElementWidth + $.sideBarImpl.iconSize;
//		            $("div.sideBar").animate({ width: animateWidthTo, duration: 600 });
//		            $.sideBarImpl.isResourcesOpen = !$.sideBarImpl.isResourcesOpen;
//		        }
//		        else {
//		            $("div.sideBar").animate({ width: $.sideBarImpl.iconSize, duration: 600 }, function () {
//		                $("#sidebarResources").hide();
//		            });
//		            $.sideBarImpl.isResourcesOpen = !$.sideBarImpl.isResourcesOpen;
//		        }
//		        if ($.sideBarImpl.isResourcesOpen) $("div.sidebarLiveDisplay").show();
//		        else $("div.sidebarLiveDisplay").hide();
//		        return false;
		    
//		},

//		handleConfigClick: function () {
		    
//		        $.sideBarImpl.isIndentOpen = false;
//		        $.sideBarImpl.isResourcesOpen = false;
//		        //$.sideBarImpl.isConfiguratiosOpen = false;
//		        $.sideBarImpl.isReportsOpen = false;
//		        $("#sidebarIndent").hide();
//		        $("#sidebarResources").hide();
//		        //$("#sidebarConfig").hide();
//		        $("#sidebarReports").hide();

//		        if (!$.sideBarImpl.isConfiguratiosOpen) {
//		            $("#sidebarConfig").show();
//		            var NewElementWidth = 300;//$("#sidebarConfig img").width();
//		            var animateWidthTo = NewElementWidth + $.sideBarImpl.iconSize;
//		            $("div.sideBar").animate({ width: animateWidthTo, duration: 600 });
//		            $.sideBarImpl.isConfiguratiosOpen = !$.sideBarImpl.isConfiguratiosOpen;

//		        }
//		        else {
//		            $("div.sideBar").animate({ width: $.sideBarImpl.iconSize, duration: 600 }, function () {
//		                $("#sidebarConfig").hide();
//		            });
//		            $.sideBarImpl.isConfiguratiosOpen = !$.sideBarImpl.isConfiguratiosOpen;
//		        }
//		        if ($.sideBarImpl.isConfiguratiosOpen) $("div.sidebarLiveDisplay").show();
//		        else $("div.sidebarLiveDisplay").hide();
//		        return false;
		    
//		},

//		closeAll: function () {
//			$("div.sideBar").animate({ width: $.sideBarImpl.iconSize, duration: 600 }, function () {
//			    $.sideBarImpl.isIndentOpen = false;
//			    $.sideBarImpl.isResourcesOpen = false;
//			    $.sideBarImpl.isConfiguratiosOpen = false;
//			    $.sideBarImpl.isReportsOpen = false;
//			    $("#sidebarIndent").hide();
//			    $("#sidebarResources").hide();
//			    $("#sidebarConfig").hide();
//			    $("#sidebarReports").hide();
//			    $("div.sidebarLiveDisplay").hide();
//			});
//		},

//	};

//	$.sideBar = function () {
//	    if (!$.sideBarImpl.isInit) {
//	        var temp = new $.sideBarImpl.init();
//	        temp.isInit = true;
//	    }
//	};

//	$.sideBar.get_isOpen = function () {
//	    return $.sideBarImpl.isIndentOpen ||
//			   $.sideBarImpl.isResourcesOpen ||
//			   $.sideBarImpl.isConfiguratiosOpen ||
//			   $.sideBarImpl.isReportsOpen;
//	};

//})(jQuery);




// https://github.com/tentonaxe/jQuery-highlightText/blob/master/jquery.highlighttext.js
// This jQuery plugin implements the $.fn.highlightText method
// making it easy to highlight text within an element by wrapping
// it in a span with a given class.
//
// To use this plugin, first select the element that contains the text
// that you want to replace, then call .highlightText() on it.
// the plugin has one required parameter and two optional parameters.
//
// The plugin is always called in the same way:
// $(sel).highlightText(<parameters>);
// It accepts three parameters.
//
// The first parameter decides what text will be matched. It can be a 
// string, a regular expression in string format, a regular expression
// object, or an array of words.
//
// The second parameter is the class that will be added to the matched
// text. it is optional and can be a space delimited list of classes to
// add to the element that wraps the matched text. It is set to "highlight"
// by default.
//
// The third parameter lets you decide whether or not the plugin should
// only match full matches. It is defaulted to false, meaning it will
// match partial matches. However, this third parameter is ignored if
// the first parameter is a regular expression object.
(function ($) {

	$.fn.highlightText = function () {
		// handler first parameter
		// is the first parameter a regexp?
		var re,
			hClass,
			reStr,
			argType = $.type(arguments[0]),
			defaultTagName = $.fn.highlightText.defaultTagName;

		if (argType === "regexp") {
			// first argument is a regular expression
			re = arguments[0];
		}
		// is the first parameter an array?
		else if (argType === "array") {
			// first argument is an array, generate
			// regular expression string for later use
			reStr = arguments[0].join("|");
		}
		// is the first parameter a string?
		else if (argType === "string") {
			// store string in regular expression string
			// for later use
			reStr = arguments[0];
		}
		// else, return out and do nothing because this
		// argument is required.
		else {
			return;
		}

		// the second parameter is optional, however,
		// it must be a string or boolean value. If it is 
		// a string, it will be used as the highlight class.
		// If it is a boolean value and equal to true, it 
		// will be used as the third parameter and the highlight
		// class will default to "highlight". If it is undefined,
		// the highlight class will default to "highlight" and 
		// the third parameter will default to false, allowing
		// the plugin to match partial matches.
		// ** The exception is if the first parameter is a regular
		// expression, the third parameter will be ignored.
		argType = $.type(arguments[1]);
		if (argType === "string") {
			hClass = arguments[1];
		}
		else if (argType === "boolean") {
			hClass = "highlight";
			if (reStr) {
				reStr = "\\b" + reStr + "\\b";
			}
		}
		else {
			hClass = "highlight";
		}

		if (arguments[2] && reStr) {
			reStr = reStr = "\\b" + reStr + "\\b";
		}

		// if re is not defined ( which means either an array or
		// string was passed as the first parameter ) create the
		// regular expression.
		if (!re) {
			re = new RegExp("(" + reStr + ")", "ig");
		}

		// iterate through each matched element
		return this.each(function () {
			// select all contents of this element
			$(this).find("*").andSelf().contents()

			// filter to only text nodes that aren't already highlighted
			.filter(function () {
				return this.nodeType === 3 && $(this).closest("." + hClass).length === 0;
			})

			// loop through each text node
			.each(function () {
				var output;
				output = this.nodeValue
					.replace(re, "<" + defaultTagName + " class='" + hClass + "'>$1</" + defaultTagName + ">");
				if (output !== this.nodeValue) {
					$(this).wrap("<p></p>").parent()
						.html(output).contents().unwrap();
				}
			});
		});
	};

	$.fn.highlightText.defaultTagName = "span";

})(jQuery);
