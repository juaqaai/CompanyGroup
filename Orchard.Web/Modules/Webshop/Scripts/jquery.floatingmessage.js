/*!
 * Floating Message v1.0.1
 * http://sideroad.secret.jp/
 *
 * Copyright (c) 2009 sideroad
 *
 * Dual licensed under the MIT or GPL Version 2 licenses.
 * Date: 2009-08-18
 * 
 * @author sideroad
 * @require jQuery
 * 
 */
( function( $ ) {
    var range = {
            top : {
                left : 10,
                right : 10
            },
            bottom : {
                left : 10,
                right : 10
            }
        },
        right = 10,
        fContainer = [],
        scrollTop = 0,
        remove;

    $( document ).ready( function() {
        scrollTop = $( window ).scrollTop();
        $( window ).scroll( function() {
            scrollTop = $( window ).scrollTop();
            for ( i = 0; i < fContainer.length; i++ ) {
                var animate = {};
                var e = fContainer[i];
                animate[e.verticalAlign] = e.range;
                if ( e.verticalAlign == "top" )
                    animate[e.verticalAlign] += scrollTop;
                else animate[e.verticalAlign] -= scrollTop;

                e.elem.animate( animate, {
                    duration : e.moveEaseTime,
                    easing : e.moveEasing,
                    queue : false
                } );
            }
        } );
    } );

    $.floatingMessage = function( message, options ) {
        var id = "jqueryFloatingMessage" + new Date().getTime(),
            /*elem = $( '<div id="'+id+'" class="ui-widget-content ui-corner-all floating-message"></div>' ),*/
			elem = $( '<div id="cus_floatingmessage" class="ui-widget-content ui-corner-all floating-message"></div>' ),
            css = {},
            timerId = false;
        
        remove = function( elem ) {
            if ( timerId ) clearTimeout( timerId );
            var e,
                id = elem.attr("id"),
                animate = {},
                deleteIndex = 0,
                orange = ( options.height + options.margin + ( options.padding * 2 ) );
                
            for ( i = 0; i < fContainer.length; i++ ) {
                e = fContainer[i];
                if ( id === e.id ) deleteIndex = i;
                if ( e.range > options.range && e.align == options.align
                        && e.verticalAlign == options.verticalAlign ) {
                    e.range -= orange;
                    if ( e.range < 0 ) e.range = 0;
                    animate[e.verticalAlign] = e.range;
                    if ( e.verticalAlign == "top" )
                        animate[e.verticalAlign] += scrollTop;
                    else animate[e.verticalAlign] -= scrollTop;
                    e.elem.animate( animate, {
                        duration : options.stuffEaseTime,
                        easing : options.stuffEasing,
                        queue : false
                    } );
                }
            }
            fContainer.splice( deleteIndex, 1 );
            range[options.verticalAlign][options.align] -= ( options.height + options.margin + ( options.padding * 2 ) );
            if ( range[options.verticalAlign][options.align] < 0 )
                range[options.verticalAlign][options.align] = 10;
            elem.hide( options.hide, function() {
                $( this ).remove;
                if ( options.close ) options.close();
            } );
        };
            
        options = options || {};
		
        // default setting
        options = $.extend( true, {
            align : (options.position || "left-top").split("-")[0],
            verticalAlign : (options.position || "left-top").split("-")[1],
            width : 300,
            height : 50,
            time : false,
            show : "drop",
            hide : "drop",
            padding : 10,
            margin : 10,
            stuffEaseTime : 1000,
            stuffEasing : "easeOutBounce",
            moveEaseTime : 500,
            moveEasing : "easeInOutCubic",
            element : $( "<div></div>" ),
            close : false,
            click : remove,
            elem : elem,
            id : id
        }, options );
        options.range = range[options.verticalAlign][options.align];

        if ( message ) options.element.html( message.replace( /\n/g, "<br />" ) );

        css = {
            width : options.width + "px",
            height : options.height + "px",
            position : "absolute",
            padding : options.padding + "px"
        };
        css[options.verticalAlign] = range[options.verticalAlign][options.align];
        css[options.align] = right;
        if ( options.verticalAlign == "top" ) {
            css[options.verticalAlign] += scrollTop;
        } else {
            css[options.verticalAlign] -= scrollTop;
        }
        elem.css( css ).append( options.element );

        if ( options.time ) timerId = setTimeout( function(){options.click( elem )}, options.time );
        if ( options.click ) elem.bind( "click.fms", function(){options.click( elem )} );

        $( document.body ).append( elem );
        elem.show( options.show );
        fContainer.push( options );
        range[options.verticalAlign][options.align] += ( options.height + options.margin + ( options.padding * 2 ) );

    }

    $.fn.floatingMessage = function( options ) {
        if( typeof options == "string" && options == "destroy") {
            remove( $(this) );
        } else {
            options = options || {};
            options.element = this;
            $.floatingMessage( false, options );
            
        }
    }
} )( jQuery );