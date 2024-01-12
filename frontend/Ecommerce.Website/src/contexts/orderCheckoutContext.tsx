import OrderCheckoutItem from '@/interfaces/OrderCheckoutItem';
import React, { createContext, useContext, useState, ReactNode } from 'react';

interface OrderCheckoutContextProps {
    orderCheckoutItems: OrderCheckoutItem[];
    setOrderCheckout: (items: OrderCheckoutItem[]) => void;
}

const OrderCheckoutContext = createContext<OrderCheckoutContextProps | undefined>(undefined);

export const OrderCheckoutProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [orderCheckoutItems, setOrderCheckoutItems] = useState<OrderCheckoutItem[]>([]);

    function setOrderCheckout(items: OrderCheckoutItem[]) {
        setOrderCheckoutItems(items);
    }

    return (
        <OrderCheckoutContext.Provider value={{ orderCheckoutItems, setOrderCheckout }}>
            {children}
        </OrderCheckoutContext.Provider>
    )
}

export const useOrderCheckout = () => {
    const context = useContext(OrderCheckoutContext);
    if (!context) {
        throw new Error('useOrderCheckout must be used within a OrderCheckoutProvider');
    }

    return context;
}
