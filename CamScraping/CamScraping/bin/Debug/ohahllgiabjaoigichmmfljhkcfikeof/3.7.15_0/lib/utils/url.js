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

(function (api, global) {

    /**
     * Helper methods to work with URLs
     */
    var UrlUtils = {

        isHttpRequest: function (url) {
            return url && url.indexOf('http') === 0;
        },

        trimHttp: function(url) {
            return url.replace(/(^\w+:|^)\/\//, '');
        },

        isHttpOrWsRequest: function (url) {
            return url && (url.indexOf('http') === 0 || url.indexOf('ws') === 0);
        },

        toPunyCode: function (domain) {
            if (!domain) {
                return "";
            }
            if (/^[\x00-\x7F]+$/.test(domain)) {
                return domain;
            }
            return global.punycode.toASCII(domain);
        },

        isThirdPartyRequest: function (requestUrl, referrer) {
            var domainName = this._get2NdLevelDomainName(requestUrl);
            var refDomainName = this._get2NdLevelDomainName(referrer);
            return domainName != refDomainName;
        },

        /**
         * Retrieves hostname from URL
         */
        getHost: function (url) {

            if (!url) {
                return null;
            }

            var firstIdx = url.indexOf("//");
            if (firstIdx === -1) {
                /**
                 * It's non hierarchical structured URL (e.g. stun: or turn:)
                 * https://tools.ietf.org/html/rfc4395#section-2.2
                 * https://tools.ietf.org/html/draft-nandakumar-rtcweb-stun-uri-08#appendix-B
                 */
                firstIdx = url.indexOf(":");
                if (firstIdx === -1) {
                    return null;
                }
                firstIdx = firstIdx - 1;
            }

            var nextSlashIdx = url.indexOf("/", firstIdx + 2);
            var startParamsIdx = url.indexOf("?", firstIdx + 2);

            var lastIdx = nextSlashIdx;
            if (startParamsIdx > 0 && (startParamsIdx < nextSlashIdx || nextSlashIdx < 0)) {
                lastIdx = startParamsIdx;
            }

            var host = lastIdx === -1 ? url.substring(firstIdx + 2) : url.substring(firstIdx + 2, lastIdx);

            var portIndex = host.indexOf(":");
            return portIndex === -1 ? host : host.substring(0, portIndex);
        },

        getDomainName: function (url) {
            var host = this.getHost(url);
            return this.getCroppedDomainName(host);
        },

        getCroppedDomainName: function (host) {
            return api.strings.startWith(host, "www.") ? host.substring(4) : host;
        },

        isIpv4: function (address) {
            if (RE_V4.test(address)) {
                return true;
            }
            if (RE_V4_HEX.test(address)) {
                return true;
            }
            if (RE_V4_NUMERIC.test(address)) {
                return true;
            }
            return false;
        },

        isIpv6: function (address) {

            var a4addon = 0;
            var address4 = address.match(RE_V4inV6);
            if (address4) {
                var temp4 = address4[0].split('.');
                for (var i = 0; i < 4; i++) {
                    if (/^0[0-9]+/.test(temp4[i])) {
                        return false;
                    }
                }
                address = address.replace(RE_V4inV6, '');
                if (/[0-9]$/.test(address)) {
                    return false;
                }

                address = address + temp4.join(':');
                a4addon = 2;
            }

            if (RE_BAD_CHARACTERS.test(address)) {
                return false;
            }

            if (RE_BAD_ADDRESS.test(address)) {
                return false;
            }

            function count(string, substring) {
                return (string.length - string.replace(new RegExp(substring, "g"), '').length) / substring.length;
            }

            var halves = count(address, '::');
            if (halves == 1 && count(address, ':') <= 6 + 2 + a4addon) {
                return true;
            }

            if (halves == 0 && count(address, ':') == 7 + a4addon) {
                return true;
            }

            return false;
        },

        urlEquals: function (u1, u2) {
            if (!u1 || !u2) {
                return false;
            }
            u1 = u1.split(/[#?]/)[0];
            u2 = u2.split(/[#?]/)[0];
            return u1 == u2;
        },

        /**
         * Checks all domains from domainNames with isDomainOrSubDomain
         * @param domainNameToCheck Domain name to check
         * @param domainNames List of domain names
         * @returns boolean true if there is suitable domain in domainNames
         */
        isDomainOrSubDomainOfAny: function (domainNameToCheck, domainNames) {
            if (!domainNames || domainNames.length == 0) {
                return false;
            }

            for (var i = 0; i < domainNames.length; i++) {
                if (this.isDomainOrSubDomain(domainNameToCheck, domainNames[i])) {
                    return true;
                }
            }

            return false;
        },

        /**
         * Checks if the specified domain is a sub-domain of equal to domainName
         *
         * @param domainNameToCheck Domain name to check
         * @param domainName        Domain name
         * @returns boolean true if there is suitable domain in domainNames
         */
        isDomainOrSubDomain: function (domainNameToCheck, domainName) {
            // Double endsWith check is memory optimization
            // Works in android, not sure if it makes sense here
            return domainName == domainNameToCheck ||
                api.strings.endsWith(domainNameToCheck, domainName) &&
                api.strings.endsWith(domainNameToCheck, "." + domainName);
        },

        _get2NdLevelDomainName: function (url) {

            if(Object.keys(RESERVED_DOMAINS).length === 0) {
                loadReservedDomains().then(() => {
                    return this._get2NdLevelDomainName(url);
                });
                return;
            }

            var host = this.getHost(url);

            if (!host) {
                return null;
            }

            var parts = host.split(".");
            if (parts.length <= 2) {
                return host;
            }

            var twoPartDomain = parts[parts.length - 2] + "." + parts[parts.length - 1];
            var isContainsTwoLvlPostfix = (twoPartDomain in RESERVED_DOMAINS);

            var threePartDomain = parts[parts.length - 3] + "." + twoPartDomain;
            if (parts.length == 3 && isContainsTwoLvlPostfix) {
                return threePartDomain;
            }
            if (threePartDomain in RESERVED_DOMAINS) {
                if (parts.length == 3) {
                    return threePartDomain;
                }
                return parts[parts.length - 4] + "." + threePartDomain;
            }

            return isContainsTwoLvlPostfix ? threePartDomain : twoPartDomain;
        }
    };

    var RE_V4 = /^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|0x[0-9a-f][0-9a-f]?|0[0-7]{3})\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|0x[0-9a-f][0-9a-f]?|0[0-7]{3})$/i;
    var RE_V4_HEX = /^0x([0-9a-f]{8})$/i;
    var RE_V4_NUMERIC = /^[0-9]+$/;
    var RE_V4inV6 = /(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$/;

    var RE_BAD_CHARACTERS = /([^0-9a-f:])/i;
    var RE_BAD_ADDRESS = /([0-9a-f]{5,}|:{3,}|[^:]:$|^:[^:]$)/i;

    var RESERVED_DOMAINS = {};

    const loadReservedDomains = () => new Promise((resolve, reject) => {

        const url = abu.getURL(`filters/public_suffix_list.json`);
        const success = function (response) {
            if (response && response.responseText) {
                const suffixList = abu.backend.parseJson(response.responseText);

                if (!suffixList) {
                    reject(createError('invalid response', url, response));
                    return;
                }

                for(let i = 0, c = suffixList.length; i < c; i++) {
                    RESERVED_DOMAINS[suffixList[i]] = 1;
                }

                resolve();
            } else {
                reject(createError('empty response', url, response));
            }
        };

        const error = (request, ex) => {
            const exMessage = (ex && ex.message) || 'couldn\'t load local suffix list';
            reject(createError(exMessage, url, request));
        };

        abu.backend.executeRequestAsync(url, 'application/json', success, error);
    });

    api.url = UrlUtils;

})(abu.utils, window);


