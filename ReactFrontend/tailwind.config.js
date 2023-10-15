const colors = require("tailwindcss/colors");

module.exports = {
    content: ["./src/**/*.{html,js}"],
    theme: {
        colors: {
            stone: colors.stone,
            white: colors.white,
            red: colors.red,
            green: colors.green
        },
        fontFamily: {
            sans: ["sans"],
            serif: ["Lora", "serif"]
        },
        extend: {}
    },
    plugins: [
        require('tailwind-scrollbar'),
    ],
    extend: {
    }
};
