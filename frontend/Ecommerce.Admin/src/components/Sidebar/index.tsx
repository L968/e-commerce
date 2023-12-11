import PaymentIcon from '@mui/icons-material/Payment';
import ExpandMore from '@mui/icons-material/ExpandMore';
import ExpandLess from '@mui/icons-material/ExpandLess';
import InventoryIcon from '@mui/icons-material/Inventory';
import DashboardIcon from '@mui/icons-material/Dashboard';
import ShoppingCartIcon from '@mui/icons-material/ShoppingCart';
import { Collapse, List, ListItemButton, ListItemIcon, ListItemText } from "@mui/material";

import { Fragment, useState } from 'react';
import { useRouter } from 'next/router';
import { ChildListItemButton, Drawer } from './styles';

interface MenuItem {
    text: string;
    icon: JSX.Element;
    children?: { text: string, src: string }[];
    src?: string;
}

const menuItems: MenuItem[] = [
    {
        text: 'Dashboard',
        icon: <DashboardIcon />,
        src: '/dashboard',
    },
    {
        text: 'Products',
        icon: <InventoryIcon />,
        children: [
            { text: 'Add Product', src: '/add-product' },
            { text: 'Product List', src: '/products' },
            { text: 'Categories', src: '/categories' },
        ]
    },
    {
        text: 'Orders',
        icon: <ShoppingCartIcon />,
        src: '/orders',
    },
    {
        text: 'Transactions',
        icon: <PaymentIcon />,
        src: '/transactions'
    },
];

export default function Sidebar() {
    const router = useRouter();
    const [expandedItems, setExpandedItems] = useState<string[]>([]);

    function handleOnClick(text: string, src?: string) {
        if (src) {
            router.push(src);
        } else {
            if (expandedItems.includes(text)) {
                setExpandedItems(expandedItems.filter(item => item !== text));
            } else {
                setExpandedItems([...expandedItems, text]);
            }
        }
    };

    function handleChildeOnClick(childSrc: string) {
        router.push(childSrc);
    }

    return (
        <Drawer variant="permanent">
            <List>
                {menuItems.map(item =>
                    <Fragment key={item.text}>
                        <ListItemButton onClick={() => handleOnClick(item.text, item.src)}>
                            <ListItemIcon>{item.icon}</ListItemIcon>
                            <ListItemText>{item.text}</ListItemText>
                            {item.children &&
                                <>
                                    {expandedItems.includes(item.text) ? <ExpandLess /> : <ExpandMore />}
                                </>
                            }
                        </ListItemButton>

                        {item.children &&
                            <Collapse in={expandedItems.includes(item.text)} timeout='auto' unmountOnExit>
                                <List disablePadding>
                                    {item.children.map((child, i) =>
                                        <ChildListItemButton
                                            key={i}
                                            onClick={() => handleChildeOnClick(child.src)}
                                        >
                                            <ListItemText primaryTypographyProps={{ fontSize: '14px' }}>
                                                {child.text}
                                            </ListItemText>
                                        </ChildListItemButton>
                                    )}
                                </List>
                            </Collapse>
                        }
                    </Fragment>
                )}
            </List>
        </Drawer>
    )
}
