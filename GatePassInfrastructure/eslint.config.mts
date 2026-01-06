import globals from 'globals';
import tseslint from 'typescript-eslint';
import { defineConfig } from 'eslint/config';

export default defineConfig([
	{
		files: ['**/*.{js,mjs,cjs,ts,mts,cts}'],
		languageOptions: { globals: globals.browser },
	},
	{
		rules: {
			semi: ['error', 'always'],
			curly: ['error', 'all'],
			'space-before-function-paren': ['error', 'always'],
			'keyword-spacing': ['error', { 'before': true, 'after': true }],
			'no-extra-parens': ['off'],
			'comma-dangle': ['error', {
				'arrays': 'always-multiline',
				'objects': 'always-multiline',
				'imports': 'never',
				'exports': 'never',
				'functions': 'never',
			}],
			quotes: ['error', 'single', { 'allowTemplateLiterals': true }],
			'space-before-blocks': ['error', 'always'],
			'no-trailing-spaces': ['error'],
			'@typescript-eslint/no-explicit-any': 'off',
			'@typescript-eslint/no-var-requires': 'off',
			'@typescript-eslint/ban-ts-comment': 'off',
			'operator-linebreak': ['error', 'before'],
			'max-len': ['error', { code: 180 }],
			'brace-style': ['error', '1tbs'],
			'padded-blocks': ['error', 'never'],
			'no-multiple-empty-lines': ['error', {
				max: 1,
				maxEOF: 0,
				maxBOF: 0,
			}],
		},
	},
	tseslint.configs.recommended,
]);
