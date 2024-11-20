module.exports = {
  content: [
    './*.html',
    './**/*.{js,jsx,ts,tsx}', 
  ],
  theme: {
    extend: {
      fontFamily: {
        'host-grotesk': ['"Host Grotesk"', 'sans-serif'], // تعريف الخط في Tailwind
      },
    },
  },
  plugins: [],
};
