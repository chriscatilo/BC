(function () {
	angular.module("eqcs.core.service")
        .value('fileUploaderOptions', {
        	url: '/',
        	alias: 'file',
        	headers: {},
        	queue: [],
        	progress: 0,
        	removeAfterUpload: false,
        	method: 'POST',
        	filters: [],
        	formData: [],
        	queueLimit: Number.MAX_VALUE,
        	withCredentials: false
        })
        .factory("fileUploaderService", [
            'fileUploaderOptions', '$rootScope', '$http', '$window', '$compile',
            function (fileUploaderOptions, $rootScope, $http, $window, $compile) {
            	// Create an instance of FileUploaderService
            	function FileUploader(options) {
            		var settings = angular.copy(fileUploaderOptions);
            		angular.extend(this, settings, options, {
            			isUploading: false,
            			_nextIndex: 0,
            			_failFilterIndex: -1,
            			_directives: { select: [] }
            		});

            		// add default filters
            		this.filters.unshift({ name: 'queueLimit', fn: this._queueLimitFilter });
            		this.filters.unshift({ name: 'folder', fn: this._folderFilter });

            	};

            	FileUploader.prototype.isHTML5 = !!($window.File && $window.FormData);
            	FileUploader.prototype.addToQueue = function (files, options, filters) {
            		var list = this.isArrayLikeObject(files) ? files : [files];
            		var arrayOfFilters = this._getFilters(filters);
            		var addedFileItems = [];

            		angular.forEach(list, function (some /*{File|HTMLInputElement|Object}*/) {
            			var temp = new FileUploader.FileLikeObject(some);

            			if (this._isValidFile(temp, arrayOfFilters, options)) {
            				var fileItem = new FileUploader.FileItem(this, some, options);
            				addedFileItems.push(fileItem);
            				this.queue.push(fileItem);
            				this._onAfterAddingFile(fileItem);

            				// upload the file straight away.
            				this.uploadItem(fileItem);
            			} else {
            				var filter = this.filters[this._failFilterIndex];
            				this._onWhenAddingFileFailed(temp, filter, options);
            			}
            		}, this);

            		this._render();
            	};
            	FileUploader.prototype.removeFromQueue = function (value) {
            		var index = this.getIndexOfItem(value);
            		var item = this.queue[index];
            		if (item.isUploading) item.cancel();
            		this.queue.splice(index, 1);
            		item._destroy();
            		this.progress = this._getTotalProgress();
            	};

            	FileUploader.prototype.removeUploadedFile = function (value) {
            	    var index = this.getIndexOfItem(value);
            	    this.queue.splice(index, 1);
            	}
               
                FileUploader.prototype.clearQueue = function () {
            		while (this.queue.length) {
            			this.queue[0].remove();
            		}
            		this.progress = 0;
            	};
            	FileUploader.prototype.uploadItem = function (value) {
            		var index = this.getIndexOfItem(value);
            		var item = this.queue[index];
            		var transport = this.isHTML5 ? '_xhrTransport' : '_iframeTransport';

            		item._prepareToUploading();
            		if (this.isUploading) return;

            		this.isUploading = true;
            		this[transport](item);
            	};
            	FileUploader.prototype.cancelItem = function (value) {
            		var index = this.getIndexOfItem(value);
            		var item = this.queue[index];
            		var prop = this.isHTML5 ? '_xhr' : '_form';
            		if (item && item.isUploading) item[prop].abort();
            	};
            	FileUploader.prototype.isFile = function (value) {
            		var fn = $window.File;
            		return (fn && value instanceof fn);
            	};
            	FileUploader.prototype.isFileLikeObject = function (value) {
            		return value instanceof FileUploader.FileLikeObject;
            	};
            	FileUploader.prototype.isArrayLikeObject = function (value) {
            		return (angular.isObject(value) && 'length' in value);
            	};
            	FileUploader.prototype.getIndexOfItem = function (value) {
            		return angular.isNumber(value) ? value : this.queue.indexOf(value);
            	};
            	FileUploader.prototype.getNotUploadedItems = function () {
            		return this.queue.filter(function (item) {
            			return !item.isUploaded;
            		});
            	};
            	FileUploader.prototype.getReadyItems = function () {
            		return this.queue
                        .filter(function (item) {
                        	return (item.isReady && !item.isUploading);
                        })
                        .sort(function (item1, item2) {
                        	return item1.index - item2.index;
                        });
            	};
            	FileUploader.prototype.destroy = function () {
            		angular.forEach(this._directives, function (key) {
            			angular.forEach(this._directives[key], function (object) {
            				object.destroy();
            			}, this);
            		}, this);
            	};
            	FileUploader.prototype.onAfterAddingFile = function (fileItem) { };
            	FileUploader.prototype.onWhenAddingFileFailed = function (item, filter, options) { };
            	FileUploader.prototype.onBeforeUploadItem = function (fileItem) { };
            	FileUploader.prototype.onProgressItem = function (fileItem, progress) { };
            	FileUploader.prototype.onSuccessItem = function (item, response, status, headers) { };
            	FileUploader.prototype.onErrorItem = function (item, response, status, headers) { };
            	FileUploader.prototype.onCancelItem = function (item, response, status, headers) { };
            	FileUploader.prototype.onCompleteItem = function (item, response, status, headers) { };
            	FileUploader.prototype._getTotalProgress = function (value) {
            		if (this.removeAfterUpload) return value || 0;
            		return 100;
            	};
            	FileUploader.prototype._getFilters = function (filters) {
            		if (angular.isUndefined(filters)) return this.filters;
            		if (angular.isArray(filters)) return filters;
            		var names = filters.match(/[^\s,]+/g);
            		return this.filters.filter(function (filter) {
            			return names.indexOf(filter.name) !== -1;
            		}, this);
            	};
            	FileUploader.prototype._render = function () {
            		if (!$rootScope.$$phase) $rootScope.$apply();
            	};
            	FileUploader.prototype._folderFilter = function (item) {
            		return !!(item.size || item.type);
            	};
            	FileUploader.prototype._queueLimitFilter = function () {
            		return this.queue.length < this.queueLimit;
            	};
            	FileUploader.prototype._isValidFile = function (file, filters, options) {
            		this._failFilterIndex = -1;
            		return !filters.length ? true : filters.every(function (filter) {
            			this._failFilterIndex++;
            			return filter.fn.call(this, file, options);
            		}, this);
            	};
            	FileUploader.prototype._isSuccessCode = function (status) {
            		return (status >= 200 && status < 300) || status === 304;
            	};
            	FileUploader.prototype._transformResponse = function (response, headers) {
            		var headersGetter = this._headersGetter(headers);
            		angular.forEach($http.defaults.transformResponse, function (transformFn) {
            			response = transformFn(response, headersGetter);
            		});
            		return response;
            	};
            	FileUploader.prototype._parseHeaders = function (headers) {
            		var parsed = {}, key, val, i;

            		if (!headers) return parsed;

            		angular.forEach(headers.split('\n'), function (line) {
            			i = line.indexOf(':');
            			key = line.slice(0, i).trim().toLowerCase();
            			val = line.slice(i + 1).trim();

            			if (key) {
            				parsed[key] = parsed[key] ? parsed[key] + ', ' + val : val;
            			}
            		});

            		return parsed;
            	};
            	FileUploader.prototype._headersGetter = function (parsedHeaders) {
            		return function (name) {
            			if (name) {
            				return parsedHeaders[name.toLowerCase()] || null;
            			}
            			return parsedHeaders;
            		};
            	};
            	FileUploader.prototype._xhrTransport = function (item) {
            		var xhr = item._xhr = new XMLHttpRequest();
            		var form = new FormData();
            		var that = this;

            		that._onBeforeUploadItem(item);

            		angular.forEach(item.formData, function (obj) {
            			angular.forEach(obj, function (value, key) {
            				form.append(key, value);
            			});
            		});

            		form.append(item.alias, item._file, item.file.name);

            		xhr.upload.onprogress = function (event) {
            			var progress = Math.round(event.lengthComputable ? event.loaded * 100 / event.total : 0);
            			that._onProgressItem(item, progress);
            		};

            		xhr.onload = function () {
            			var headers = that._parseHeaders(xhr.getAllResponseHeaders());
            			var response = that._transformResponse(xhr.response, headers);
            			var gist = that._isSuccessCode(xhr.status) ? 'Success' : 'Error';
            			var method = '_on' + gist + 'Item';
            			that[method](item, response, xhr.status, headers);
            			that._onCompleteItem(item, response, xhr.status, headers);
            		};

            		xhr.onerror = function () {
            			var headers = that._parseHeaders(xhr.getAllResponseHeaders());
            			var response = that._transformResponse(xhr.response, headers);
            			that._onErrorItem(item, response, xhr.status, headers);
            			that._onCompleteItem(item, response, xhr.status, headers);
            		};

            		xhr.onabort = function () {
            			var headers = that._parseHeaders(xhr.getAllResponseHeaders());
            			var response = that._transformResponse(xhr.response, headers);
            			that._onCancelItem(item, response, xhr.status, headers);
            			that._onCompleteItem(item, response, xhr.status, headers);
            		};

            		xhr.open(item.method, item.url, true);

            		xhr.withCredentials = item.withCredentials;

            		angular.forEach(item.headers, function (value, name) {
            			xhr.setRequestHeader(name, value);
            		});

            		xhr.send(form);
            		this._render();
            	};
            	FileUploader.prototype._iframeTransport = function (item) {
            		var form = angular.element('<form style="display: none;" />');
            		var iframe = angular.element('<iframe name="iframeTransport' + Date.now() + '">');
            		var input = item._input;
            		var that = this;

            		if (item._form) item._form.replaceWith(input); // remove old form
            		item._form = form; // save link to new form

            		that._onBeforeUploadItem(item);

            		input.prop('name', item.alias);

            		angular.forEach(item.formData, function (obj) {
            			angular.forEach(obj, function (value, key) {
            				var element = angular.element('<input type="hidden" name="' + key + '" />');
            				element.val(value);
            				form.append(element);
            			});
            		});

            		form.prop({
            			action: item.url,
            			method: 'POST',
            			target: iframe.prop('name'),
            			enctype: 'multipart/form-data',
            			encoding: 'multipart/form-data' // old IE
            		});

            		iframe.bind('load', function () {
            			try {
            				var html = iframe[0].contentDocument.body.innerHTML;
            			} catch (e) {
            			}

            			var xhr = { response: html, status: 200, dummy: true };
            			var headers = {};
            			var response = that._transformResponse(xhr.response, headers);

            			that._onSuccessItem(item, response, xhr.status, headers);
            			that._onCompleteItem(item, response, xhr.status, headers);
            		});

            		form.abort = function () {
            			var xhr = { status: 0, dummy: true };
            			var headers = {};
            			var response;

            			iframe.unbind('load').prop('src', 'javascript:false;');
            			form.replaceWith(input);

            			that._onCancelItem(item, response, xhr.status, headers);
            			that._onCompleteItem(item, response, xhr.status, headers);
            		};

            		input.after(form);
            		form.append(input).append(iframe);

            		form[0].submit();
            		this._render();
            	};
            	FileUploader.prototype._onWhenAddingFileFailed = function (item, filter, options) {
            		this.onWhenAddingFileFailed(item, filter, options);
            	};
            	FileUploader.prototype._onAfterAddingFile = function (item) {
            		this.onAfterAddingFile(item);
            	};
            	FileUploader.prototype._onBeforeUploadItem = function (item) {
            		item._onBeforeUpload();
            		this.onBeforeUploadItem(item);
            	};
            	FileUploader.prototype._onProgressItem = function (item, progress) {
            		var total = this._getTotalProgress(progress);
            		this.progress = total;
            		item._onProgress(progress);
            		this.onProgressItem(item, progress);
            		this._render();
            	};
            	FileUploader.prototype._onSuccessItem = function (item, response, status, headers) {
            		item._onSuccess(response, status, headers);
            		this.onSuccessItem(item, response, status, headers);
            	};
            	FileUploader.prototype._onErrorItem = function (item, response, status, headers) {
            		item._onError(response, status, headers);
            		this.onErrorItem(item, response, status, headers);
            	};
            	FileUploader.prototype._onCancelItem = function (item, response, status, headers) {
            		item._onCancel(response, status, headers);
            		this.onCancelItem(item, response, status, headers);
            	};
            	FileUploader.prototype._onCompleteItem = function (item, response, status, headers) {
            		item._onComplete(response, status, headers);
            		this.onCompleteItem(item, response, status, headers);

            		var nextItem = this.getReadyItems()[0];
            		this.isUploading = false;

            		if (angular.isDefined(nextItem)) {
            			nextItem.upload();
            			return;
            		}

            		this.progress = this._getTotalProgress();
            		this._render();
            	};
            	FileUploader.isFile = FileUploader.prototype.isFile;
            	FileUploader.isFileLikeObject = FileUploader.prototype.isFileLikeObject;
            	FileUploader.isArrayLikeObject = FileUploader.prototype.isArrayLikeObject;
            	FileUploader.isHTML5 = FileUploader.prototype.isHTML5;
            	FileUploader.inherit = function (target, source) {
            		target.prototype = Object.create(source.prototype);
            		target.prototype.constructor = target;
            		target.super_ = source;
            	};
            	FileUploader.FileLikeObject = FileLikeObject;
            	FileUploader.FileItem = FileItem;
            	FileUploader.FileDirective = FileDirective;
            	FileUploader.FileSelect = FileSelect;

            	function FileLikeObject(fileOrInput) {
            		var isInput = angular.isElement(fileOrInput);
            		var fakePathOrObject = isInput ? fileOrInput.value : fileOrInput;
            		var postfix = angular.isString(fakePathOrObject) ? 'FakePath' : 'Object';
            		var method = '_createFrom' + postfix;
            		this[method](fakePathOrObject);
            	}

            	FileLikeObject.prototype._createFromFakePath = function (path) {
            		this.lastModifiedDate = null;
            		this.size = null;
            		this.type = 'like/' + path.slice(path.lastIndexOf('.') + 1).toLowerCase();
            		this.name = path.slice(path.lastIndexOf('/') + path.lastIndexOf('\\') + 2);
            	};
            	FileLikeObject.prototype._createFromObject = function (object) {
            		this.lastModifiedDate = angular.copy(object.lastModifiedDate);
            		this.size = object.size;
            		this.type = object.type;
            		this.name = object.name;
            	};

            	function FileItem(uploader, some, options) {
            		var isInput = angular.isElement(some);
            		var input = isInput ? angular.element(some) : null;
            		var file = !isInput ? some : null;

            		angular.extend(this, {
            			url: uploader.url,
            			alias: uploader.alias,
            			headers: angular.copy(uploader.headers),
            			formData: angular.copy(uploader.formData),
            			removeAfterUpload: uploader.removeAfterUpload,
            			withCredentials: uploader.withCredentials,
            			method: uploader.method
            		}, options, {
            			uploader: uploader,
            			file: new FileUploader.FileLikeObject(some),
            			isReady: false,
            			isUploading: false,
            			isUploaded: false,
            			isSuccess: false,
            			isCancel: false,
            			isError: false,
            			progress: 0,
            			index: null,
            			_file: file,
            			_input: input
            		});

            		if (input) this._replaceNode(input);
            	}
            	FileItem.prototype.upload = function () {
            		this.uploader.uploadItem(this);
            	};
            	FileItem.prototype.cancel = function () {
            		this.uploader.cancelItem(this);
            	};
            	FileItem.prototype.remove = function () {
            		this.uploader.removeFromQueue(this);
            	};
            	FileItem.prototype.onBeforeUpload = function () { };
            	FileItem.prototype.onProgress = function (progress) { };
            	FileItem.prototype.onSuccess = function (response, status, headers) { };
            	FileItem.prototype.onError = function (response, status, headers) { };
            	FileItem.prototype.onCancel = function (response, status, headers) { };
            	FileItem.prototype.onComplete = function (response, status, headers) { };
            	FileItem.prototype._onBeforeUpload = function () {
            		this.isReady = true;
            		this.isUploading = true;
            		this.isUploaded = false;
            		this.isSuccess = false;
            		this.isCancel = false;
            		this.isError = false;
            		this.progress = 0;
            		this.onBeforeUpload();
            	};
            	FileItem.prototype._onProgress = function (progress) {
            		this.progress = progress;
            		this.onProgress(progress);
            	};
            	FileItem.prototype._onSuccess = function (response, status, headers) {
            		this.isReady = false;
            		this.isUploading = false;
            		this.isUploaded = true;
            		this.isSuccess = true;
            		this.isCancel = false;
            		this.isError = false;
            		this.progress = 100;
            		this.index = null;
            		this.onSuccess(response, status, headers);
            	};
            	FileItem.prototype._onError = function (response, status, headers) {
            		this.isReady = false;
            		this.isUploading = false;
            		this.isUploaded = true;
            		this.isSuccess = false;
            		this.isCancel = false;
            		this.isError = true;
            		this.progress = 0;
            		this.index = null;
            		this.onError(response, status, headers);
            	};
            	FileItem.prototype._onCancel = function (response, status, headers) {
            		this.isReady = false;
            		this.isUploading = false;
            		this.isUploaded = false;
            		this.isSuccess = false;
            		this.isCancel = true;
            		this.isError = false;
            		this.progress = 0;
            		this.index = null;
            		this.onCancel(response, status, headers);
            	};
            	FileItem.prototype._onComplete = function (response, status, headers) {
            		this.onComplete(response, status, headers);
            		if (this.removeAfterUpload) this.remove();
            	};
            	FileItem.prototype._destroy = function () {
            		if (this._input) this._input.remove();
            		if (this._form) this._form.remove();
            		delete this._form;
            		delete this._input;
            	};
            	FileItem.prototype._prepareToUploading = function () {
            		this.index = this.index || ++this.uploader._nextIndex;
            		this.isReady = true;
            	};
            	FileItem.prototype._replaceNode = function (input) {
            		var clone = $compile(input.clone())(input.scope());
            		clone.prop('value', null); // FF fix
            		input.css('display', 'none');
            		input.after(clone); // remove jquery dependency
            	};

            	function FileDirective(options) {
            		angular.extend(this, options);
            		this.uploader._directives[this.prop].push(this);
            		this._saveLinks();
            		this.bind();
            	}

            	FileDirective.prototype.events = {};
            	FileDirective.prototype.bind = function () {
            		for (var key in this.events) {
            			var prop = this.events[key];
            			this.element.bind(key, this[prop]);
            		}
            	};
            	FileDirective.prototype.unbind = function () {
            		for (var key in this.events) {
            			this.element.unbind(key, this.events[key]);
            		}
            	};
            	FileDirective.prototype.destroy = function () {
            		var index = this.uploader._directives[this.prop].indexOf(this);
            		this.uploader._directives[this.prop].splice(index, 1);
            		this.unbind();
            		// this.element = null;
            	};
            	FileDirective.prototype._saveLinks = function () {
            		for (var key in this.events) {
            			var prop = this.events[key];
            			this[prop] = this[prop].bind(this);
            		}
            	};


            	FileUploader.inherit(FileSelect, FileDirective);

            	function FileSelect(options) {
            		FileSelect.super_.apply(this, arguments);

            		if (!this.uploader.isHTML5) {
            			this.element.removeAttr('multiple');
            		}
            		this.element.prop('value', null); // FF fix
            	}

            	FileSelect.prototype.events = {
            		$destroy: 'destroy',
            		change: 'onChange'
            	};
            	FileSelect.prototype.prop = 'select';
            	FileSelect.prototype.getOptions = function () { };
            	FileSelect.prototype.getFilters = function () { };
            	FileSelect.prototype.isEmptyAfterSelection = function () {
            		return !!this.element.attr('multiple');
            	};
            	FileSelect.prototype.onChange = function () {
            		var files = this.uploader.isHTML5 ? this.element[0].files : this.element[0];
            		var options = this.getOptions();
            		var filters = this.getFilters();

            		if (!this.uploader.isHTML5) this.destroy();
            		this.uploader.addToQueue(files, options, filters);
            		if (this.isEmptyAfterSelection()) this.element.prop('value', null);
            	};

            	return FileUploader;
            }
        ])
        .directive('nvFileSelect', [
            '$parse', 'fileUploaderService', function ($parse, fileUploaderService) {
            	return {
            		link: function (scope, element, attributes) {
            			var uploader = scope.$eval(attributes.uploader);

            			if (!(uploader instanceof fileUploaderService)) {
            				throw new TypeError('"Uploader" must be an instance of FileUploader');
            			}

            			var object = new fileUploaderService.FileSelect({
            				uploader: uploader,
            				element: element
            			});

            			object.getOptions = $parse(attributes.options).bind(object, scope);
            			object.getFilters = function () { return attributes.filters; };
            		}
            	};
            }
        ]);
})();
