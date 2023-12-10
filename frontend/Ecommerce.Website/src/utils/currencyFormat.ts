export default function currencyFormat(value: number | null | undefined): string {
    if (!value) return '';

    return '$' + value.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,')
}
