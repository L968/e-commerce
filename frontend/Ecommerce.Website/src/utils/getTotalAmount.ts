interface Item {
    product: {
        discountedPrice: number;
    };
    quantity: number;
}

export default function getTotalAmount(items: Item[]): number {
    return items.reduce((total, item) =>
        total + item.product.discountedPrice * item.quantity, 0
    );
}
