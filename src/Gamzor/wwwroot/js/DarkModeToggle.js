export function setTheme(theme) {
    document.documentElement.setAttribute('data-theme', theme)
    // Add theme cookie
    document.cookie = `theme=${theme};path=/;max-age=31536000`
}

export function getTheme() {
    let theme = document.cookie.split('; ').find(row => row.startsWith('theme='))
    return theme ? theme.split('=')[1] :
    // If no cookie is set, get the system theme
    window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light'
}