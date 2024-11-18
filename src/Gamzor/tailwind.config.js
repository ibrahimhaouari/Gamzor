module.exports = {
    content: ['../**/*.{razor,html,js,razor.css}'],
    darkMode: ['selector', '[data-theme="dark"]'],
    theme: {
      fontFamily: {
        'sans': ['Poppins', 'sans-serif'],
        'body': ['Mulish', 'sans-serif'],
      },
    },
    plugins: [
      require("daisyui"),
   ],
   daisyui: {
    themes: [
      {
        light: {
          ...require("daisyui/src/theming/themes")["light"],
          "primary": "#1d4ed8",
          "primary-content": "#ffffff",
          "error": "#ef4444",
          "success": "#14b8a6",
        },
      },
    "dark"],
    prefix: "",
  },
  }