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

const vbscript_html_references = [
  "xml",
  "vbscript"
];