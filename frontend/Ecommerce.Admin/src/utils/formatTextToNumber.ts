export default function formatTextToNumber(text: string, decimalPlaces = 2, allowDecimal = true): string {
    let formattedText = text.replace(/[^0-9.]/g, '');

    formattedText = formattedText.replace(/^0+(?!\.)/, '');

    const parts = formattedText.split('.');
    formattedText = parts.shift() + (parts.length ? '.' + parts.join('') : '');

    if (formattedText.startsWith('-')) {
        formattedText = formattedText.substring(1);
    }

    if (formattedText.startsWith('.') && allowDecimal) {
        formattedText = '0' + formattedText;
    }

    if (!allowDecimal) {
        formattedText = formattedText.split('.')[0];
    }

    const decimalIndex = formattedText.indexOf('.');
    if (decimalIndex !== -1) {
        const integerPart = formattedText.substring(0, decimalIndex);
        const decimalPart = formattedText.substring(decimalIndex + 1, decimalIndex + 1 + decimalPlaces);
        formattedText = integerPart + '.' + decimalPart;
    }

    return formattedText;
}
