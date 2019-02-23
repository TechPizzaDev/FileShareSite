onmessage = function (event) {
	importScripts("/resources/lib/highlight.js/highlight.js");
	const data = event.data;

	importScripts(`/resources/lib/highlight.js/languages/cs.js`);
	importScripts(`/resources/lib/highlight.js/languages/xml.js`);

	importScripts(`/resources/lib/highlight.js/languages/${data.language}.js`);
	hljs.registerLanguage(data.language, this[data.language]);
	hljs.registerLanguage("cs", this["cs"]);
	hljs.registerLanguage("xml", this["xml"]);

	hljs.configure({ tabReplace: 4 });
	let result = hljs.highlight(data.language, data.code, true);
	postMessage({ result: result.value, elementId: data.elementId });

	//close();
};

function encodeHtml(str) {
	return str
		.replace(/&/g, '&amp;')
		.replace(/"/g, '&quot;')
		.replace(/'/g, '&#39;')
		.replace(/</g, '&lt;')
		.replace(/>/g, '&gt;')
		.replace(/\//g, '&#x2F;');
}

function decodeHtml(str) {
	return str
		.replace(/&quot;/g, '"')
		.replace(/&#39;/g, "'")
		.replace(/&lt;/g, '<')
		.replace(/&gt;/g, '>')
		.replace(/&amp;/g, '&')
		.replace(/&#x2F;/g, '/');
}
