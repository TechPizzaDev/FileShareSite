importScripts("/resources/lib/highlight.js/highlight.js");
importScripts("/resources/js/he.js");
const scriptMap = {};

onmessage = function (event) {
	const data = event.data;

	loadLang(data.language);
	hljs.configure({ tabReplace: 4 });

	const srcHtml = he.decode(data.code);
	const result = hljs.highlight(data.language, srcHtml, true);
	postMessage({ result: result.value, elementId: data.elementId });

	//close();
};

function loadLang(name) {
	if (scriptMap[name] !== name) {
		scriptMap[name] = name;

		importScripts(`/resources/lib/highlight.js/languages/${name}.js`);
		hljs.registerLanguage(name, this[name]);

		const refs = this[`${name.replace('_', '-')}_references`];
		if (refs) {
			for (let i = 0; i < refs.length; i++) {
				loadLang(refs[i]);
			}
		}
	}
}