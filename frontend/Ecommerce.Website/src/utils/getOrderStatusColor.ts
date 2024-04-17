export default function getOrderStatusColor(status: string): string {
    switch (status) {
        case 'Pending Payment': return '#FF7733';
        case 'Processing': return '#00A650';
        case 'Delivered': return '#00A650';
        case 'Cancelled': return '#F23D4F';
        default: return '#000';
    }
}
