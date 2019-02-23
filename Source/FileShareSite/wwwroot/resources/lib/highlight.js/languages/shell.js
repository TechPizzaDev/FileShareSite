function shell(hljs) {
  return {
    aliases: ['console'],
    contains: [
      {
        className: 'meta',
        begin: '^\\s{0,3}[\\w\\d\\[\\]()@-]*[>%$#]',
        starts: {
          end: '$', subLanguage: 'bash'
        }
      }
    ]
  }
};

var shell_references = [
  "bash"
];
