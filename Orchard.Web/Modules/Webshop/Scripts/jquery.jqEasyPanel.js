/* jQuery Easy Panel plugin
 * Examples and documentation at: http://www.jqeasy.com/
 * Version: 1.0 (31/03/2010)
 * No license. Use it however you want. Just keep this notice included.
 * Requires: jQuery v1.3+
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.
 */
 (function($){
    $.fn.jqEasyPanel = function(givenOpts) {
        opts = $.extend({
			position: 'top',			// panel position 'top' or 'bottom'
			height: '80px',				// set the height of the panel
			speed: 'normal',			// 'slow', 'normal', 'fast', or number in milliseconds
			moveContainer: true,		// whether to move your page container along with the panel
			container: '#container',	// container id or class
			openBtn: '.open',			// open button id or class inside 'jqeasytrigger' div
			closeBtn: '.close',			// close button id or class inside 'jqeasytrigger' div
			openLink: '.openpanel',		// open button id or class for text links to open panel
			closeLink: '.closepanel',	// close button id or class for text links to close panel
			keepOpenCheck: '#keepopen',	// sets cookie value to show panel on load on all pages
			showTrigger: true,			// turn trigger tab button on/off
			showOnLoad: false			// ALWAYS open panel on page load. Ignores cookie value!	
        }, givenOpts);
		
		// refers to the selector 'jqEasyPanel'
		var $this = $(this);
		
		// vars needed to pass args to animate method
		var containerpadding;
		var aniOpenArgs = {};
		var aniCloseArgs = {};
		
		// add appropriate class names based on position
		if (opts.position == 'top') {
			$this.addClass('top');
			$('#jqeasytrigger').addClass('top');
			containerpadding = 'padding-top';
		} else {
			$this.addClass('bottom');
			$('#jqeasytrigger').addClass('bottom');
			$('#jqeasypaneloptions').css('margin-right','14px');
			containerpadding = 'padding-bottom';
		};
		
		// set panel's height
		$this.css('height', opts.height);
		
		// remove the 'px' from the height string so we can calculate the container padding
		var newpadding = opts.height.replace("px","");
		// add 25 px to the panel's height
		newpadding = parseInt(opts.height) + 25;
		
		aniOpenArgs[containerpadding] = newpadding;
		aniCloseArgs[containerpadding] = 0;
		
		// show or hide the trigger
		// add 'close panel' link to paneloptions if trigger is hidden
		if (!opts.showTrigger) {
			$('#jqeasytrigger').css('display', 'none');
			$('#jqeasypaneloptions').css('margin-right','14px');
			$('#jqeasypaneloptions').prepend('<p><a href="#" class="closepanel">Close Panel</a></p>');
		};
		
		// if the cookie is set to 1 or showOnLoad is true then show the panel and the close button
		if ((readCookie('jqEasyPanel') == "1")||(opts.showOnLoad)) {
			if (opts.moveContainer) { $(opts.container).css(containerpadding,newpadding); };
			$this.css({'display':'block'});
			$(opts.openBtn).css({'display':'none'});
			$(opts.closeBtn).css({'display':'block'});
			$(opts.keepOpenCheck).attr('checked', true);
		};
		
		// slide panel in and container down
		$(opts.openBtn).click(function(){
			$this.slideDown(opts.speed);
			if (opts.moveContainer) { $(opts.container).animate(aniOpenArgs, opts.speed); };
			return false;
		});	
		
		// slide panel in and container down when the text link is clicked
		$(opts.openLink).click(function(){
			$this.slideDown(opts.speed);
			if (opts.moveContainer) { $(opts.container).animate(aniOpenArgs, opts.speed); };
			$(opts.openBtn).css({'display':'none'});
			$(opts.closeBtn).css({'display':'block'});
			return false;
		});	
		
		// slide panel out and container up
		$(opts.closeBtn).click(function(){
			$this.slideUp(opts.speed);
			if (opts.moveContainer) { $(opts.container).animate(aniCloseArgs, opts.speed); };
			return false;
		});
		
		// slide panel out and container up when the text link is clicked
		$(opts.closeLink).click(function(){
			$this.slideUp(opts.speed);
			if (opts.moveContainer) { $(opts.container).animate(aniCloseArgs, opts.speed); };
			$(opts.openBtn).css({'display':'block'});
			$(opts.closeBtn).css({'display':'none'});
			return false;
		});
		
		// toggle trigger open and close buttons
		$('#jqeasytrigger a').click(function () {
			//alert($(this).attr('class'));
			$('#jqeasytrigger a').toggle();
			return false;
		});
		
		// set cookie to always keep the panel open
		$(opts.keepOpenCheck).click(function () {
			if ($(opts.keepOpenCheck).attr('checked')) {
				//Set a cookie with the name "jqEasyPanel", a value of "1", until the browser session expires
				createCookie('jqEasyPanel','1', 0);
				//Check that the cookie was created using the readCookie(name) function
				//alert("You have set your jqEasyPanel cookie as '" +readCookie('jqEasyPanel')+ "'");
			} else {
				//Set a cookie with the name "jqEasyPanel", a value of "0", until the browser session expires
				createCookie('jqEasyPanel','0', 0);
				//Check that the cookie was created using the readCookie(name) function
				//alert("You have set your jqEasyPanel cookie as '" +readCookie('jqEasyPanel')+ "'");
			};
		});
		
		function createCookie(name,value,days) {
			if (days) {
				var date = new Date();
				date.setTime(date.getTime()+(days*24*60*60*1000));
				var expires = "; expires="+date.toGMTString();
			} else {
				var expires = "";
			}
			document.cookie = name+"="+value+expires+"; path=/";
		}
	
		function readCookie(name) {
			var nameEQ = name + "=";
			var ca = document.cookie.split(';');
			for(var i=0;i < ca.length;i++) {
				var c = ca[i];
				while (c.charAt(0)==' ') c = c.substring(1,c.length);
				if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length,c.length);
			}
			return null;
		}
		
		function eraseCookie(name) {
			createCookie(name,"",-1);
		}
	};
})(jQuery);