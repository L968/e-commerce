export default function getOrderStatusColor(status: string): string {
    switch (status) {
        case 'Pending Payment': return '#FF7733';
        case 'Delivered': return '#00A650';
        case 'Cancelled': return '#FF0000';
        default: return '#000';
    }
}
