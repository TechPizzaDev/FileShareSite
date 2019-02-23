function vbscript_html(hljs) {
  return {
    subLanguage: 'xml',
    contains: [
      {
        begin: '<%', end: '%>',
        subLanguage: 'vbscript'
      }
    ]
  };
};

var vbscript_html_references = [
  "xml",
  "vbscript"
];
