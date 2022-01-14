/**
 * This file is part of AdBlocker Ultimate Browser Extension
 *
 * AdBlocker Ultimate Browser Extension is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * AdBlocker Ultimate Browser Extension is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with AdBlocker Ultimate Browser Extension.  If not, see <http://www.gnu.org/licenses/>.
 */

/* global $, i18n */

$(function () {

    $.fn.toggleCheckbox = function () {

        return this.each(function () {

            var checkbox = this;
            var $checkbox = $(this);

            if ($checkbox.data("toggleCheckbox")) {
                //already applied
                return;
            }

            var el = $("<div>", {class: 'sp-table-row-pseudo'}).append($('<span>', {class: 'spt-row-pseudo-handler'}));
            el.insertAfter(checkbox);

            el.on('click', function () {
                checkbox.checked = !checkbox.checked;
                $checkbox.change();
            });

            $checkbox.bind('change', function () {
                onClicked(checkbox.checked);
            });

            function onClicked(checked) {
                if (checked) {
                    el.addClass("active");
                    el.closest(".s-page-table-row").addClass("active");
                } else {
                    el.removeClass("active");
                    el.closest(".s-page-table-row").removeClass("active");
                }
            }

            $checkbox.hide();
            onClicked(checkbox.checked);

            $checkbox.data("toggleCheckbox", true);
        });
    };

    $.fn.updateCheckbox = function (checked) {

        return this.each(function () {
            var $this = $(this);
            if (checked) {
                $this.attr('checked', 'checked');
            } else {
                $this.removeAttr('checked');
            }
        });
    };

    $.fn.popupHelp = function () {

        return this.each(function () {

            var el = $(this);
            var popup = $("#" + el.attr("data-popup"));
            if (!popup || popup.length == 0) {
                return;
            }

            var w = $(window);

            function positionPopup() {

                var viewport = {
                    right: w.scrollLeft() + w.width(),
                    bottom: w.scrollTop() + w.height()
                };

                var elBounds = el.offset();

                var popupHeight = popup.outerHeight();
                var popupWidth = popup.outerWidth();

                var offsetTop = elBounds.top + 15;
                if (viewport.bottom < offsetTop + popupHeight) {
                    offsetTop = elBounds.top - popupHeight - 15;
                }

                var offsetLeft = elBounds.left + 15;
                if (viewport.right < offsetLeft + popupWidth) {
                    offsetLeft = elBounds.left - popupWidth - 15;
                }

                popup.css({
                    top: offsetTop,
                    left: offsetLeft
                });
            }

            el.on({
                mouseenter: function () {
                    positionPopup();
                    popup.removeClass("hidden");
                },
                mouseleave: function () {
                    popup.addClass("hidden");
                }
            });
        });
    };
});


/**
 * Creates HTMLElement from string
 *
 * @param {String} HTML representing a single element
 * @return {Element}
 */
function htmlToElement(html) {
    // const template = document.createElement('template');
    // html = html.trim(); // Never return a text node of whitespace as the result
    // template.innerHTML = html;
    // return template.content.firstChild;

    return $(html);
}
