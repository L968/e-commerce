import Link from 'next/link';
import Image from 'next/image';
import api from '@/services/api';
import { toast } from 'react-toastify';
import { useRouter } from 'next/router';
import { LoadingButton } from '@mui/lab';
import Address from '@/interfaces/Address';
import apiOrder from '@/services/apiOrder';
import { useEffect, useState } from 'react';
import AddIcon from '@mui/icons-material/Add';
import currencyFormat from '@/utils/currencyFormat';
import getTotalAmount from '@/utils/getTotalAmount';
import PrivateRoute from '@/components/PrivateRoute';
import PaymentMethod from '@/interfaces/PaymentMethod';
import apiAuthorization from '@/services/apiAuthorization';
import CreditCardIcon from '@mui/icons-material/CreditCard';
import NumberSelector from '@/components/NumberSelector/default';
import { useOrderCheckout } from '@/contexts/orderCheckoutContext';
import PostCheckout from '@/components/pages/checkout/PostCheckout';
import OrderCheckoutRequest from '@/interfaces/api/requests/OrderCheckoutRequest';
import { Avatar, Button, CircularProgress, Divider, Radio, RadioGroup } from '@mui/material';
import { Container, ItemContainer, ItemInfo, Main, Price, PriceContainer, PriceContainerContent, PriceContainerRow, PriceContainerTitle, ProductName, Section, SectionContainer, SectionContent, SectionTitle, StyledAddressFormControlLabel, TotalPrice } from './styles';

const shipping = 20;

function Checkout() {
    const router = useRouter();
    const { orderCheckoutItems, setOrderCheckout } = useOrderCheckout();

    if (orderCheckoutItems.length === 0) {
        router.back();
        return;
    }

    const [deliveryInfo, setDeliveryInfo] = useState<Address | null>(null);
    const [addresses, setAddresses] = useState<Address[]>([]);
    const [defaultAddressId, setDefaultAddressId] = useState<string>();
    const [radioSelectedAddressId, setRadioSelectedAddressId] = useState<string | null>(null);
    const [addressLoading, setAddressLoading] = useState<boolean>(false);

    const [paymentMethod] = useState<PaymentMethod>(PaymentMethod.Pix);
    const [orderPlaced, setOrderPlaced] = useState<boolean>(false);
    const [placeOrderLoading, setPlaceOrderLoading] = useState<boolean>(false);

    const totalAmount = getTotalAmount(orderCheckoutItems);

    useEffect(() => {
        fetchDefaultAddressId();
    }, []);

    useEffect(() => {
        if (!defaultAddressId) return;
        fetchDefaultAddress();
    }, [defaultAddressId]);

    function fetchDefaultAddressId(): void {
        apiAuthorization.get<string>('/user/defaultAddressId')
            .then(res => setDefaultAddressId(res.data))
            .catch(err => {
                if (err.response?.status !== 404) {
                    toast.error('Error 500');
                } else {
                    fetchAllAddresses();
                }
            });
    }

    function fetchDefaultAddress(): void {
        setAddressLoading(true);

        api.get<Address>(`/address/${defaultAddressId}`)
            .then(res => setDeliveryInfo(res.data))
            .catch(err => toast.error('Error 500'))
            .finally(() => setAddressLoading(false));
    }

    function fetchAllAddresses(): void {
        setAddressLoading(true);

        api.get<Address[]>(`/address`)
            .then(res => {
                const fetchedAddresses = res.data;

                if (fetchedAddresses.length === 1) {
                    setDeliveryInfo(fetchedAddresses[0]);
                } else if (fetchedAddresses.length > 1) {
                    setAddresses(fetchedAddresses);
                    setRadioSelectedAddressId(fetchedAddresses[0].id);
                } else {
                    setAddresses(fetchedAddresses);
                }
            })
            .catch(err => toast.error('Error 500'))
            .finally(() => setAddressLoading(false));
    }

    function handlePlaceOrder(): void {
        if (!deliveryInfo) {
            toast.warning('Please select a delivery address before placing the order.');
            return;
        }

        if (!paymentMethod) {
            toast.warning('Please select a payment method before placing the order.');
            return;
        }

        const data: OrderCheckoutRequest = {
            orderCheckoutItems: orderCheckoutItems.map(item => ({
                productCombinationId: item.product.id,
                quantity: item.quantity
            })),
            shippingAddressId: deliveryInfo.id,
            paymentMethod: paymentMethod,
        }

        setPlaceOrderLoading(true);

        apiOrder.post('/order', data)
            .then(res => setOrderPlaced(true))
            .catch(err => toast.error('Error 500'))
            .finally(() => setPlaceOrderLoading(false));
    }

    function handleDeleteItem(productId: string): void {
        const updatedItems = orderCheckoutItems.filter(item => item.product.id !== productId);
        setOrderCheckout(updatedItems);
    }

    function handleSelectAddress(): void {
        const address = addresses.find(a => a.id === radioSelectedAddressId);
        if (!address) return;

        setDeliveryInfo(address);
    }

    function handleChangeAddress(): void {
        setDeliveryInfo(null);
    }

    function handleAddNewAddress(): void {

    }

    function updateItemQuantity(productId: string, newQuantity: number): void {
        const updatedItems = orderCheckoutItems.map((item) => {
            if (item.product.id === productId) {
                return { ...item, quantity: newQuantity };
            }
            return item;
        });

        setOrderCheckout(updatedItems);
    }

    if (orderPlaced) {
        return <PostCheckout address={deliveryInfo!} />
    }

    return (
        <Main>
            <Container>
                <SectionContainer>
                    <Section>
                        <SectionTitle variant='h3'>1 Delivery Information</SectionTitle>
                        {addressLoading && <CircularProgress />}

                        {!addressLoading && deliveryInfo &&
                            <div>
                                <div>
                                    <div>{deliveryInfo.recipientFullName.toUpperCase()}</div>
                                    <div>{deliveryInfo.streetName}, {deliveryInfo.buildingNumber}</div>
                                    <div>
                                        {deliveryInfo.complement && `${deliveryInfo.complement}`}
                                        {deliveryInfo.neighborhood && `, ${deliveryInfo.neighborhood}`}
                                    </div>
                                    <div>{deliveryInfo.city}, {deliveryInfo.state} {deliveryInfo.postalCode}</div>
                                </div>
                                <Button onClick={handleChangeAddress} sx={{ margin: '3px 0 -15px 0' }}>
                                    Change
                                </Button>
                            </div>
                        }

                        {!addressLoading && !deliveryInfo && addresses.length > 0 &&
                            <div>
                                <RadioGroup
                                    value={radioSelectedAddressId?.toString()}
                                    onChange={(event) => setRadioSelectedAddressId(event.target.value)}
                                >
                                    {addresses.map((address) => (
                                        <StyledAddressFormControlLabel
                                            key={address.id}
                                            value={address.id}
                                            control={<Radio />}
                                            checked={address.id === radioSelectedAddressId}
                                            label={
                                                <>
                                                    <div>
                                                        <strong>{address.recipientFullName.toUpperCase()}</strong> {address.streetName}, {address.buildingNumber},
                                                        {address.complement && ` ${address.complement}, `}
                                                        {address.neighborhood && `${address.neighborhood}, `}
                                                        {address.city}, {address.state} {address.postalCode}
                                                    </div>
                                                    <div>{address.country}</div>
                                                </>
                                            }
                                        />
                                    ))}
                                </RadioGroup>

                                <Button
                                    onClick={handleSelectAddress}
                                    size='small'
                                    variant='contained'
                                >
                                    Ship to this address
                                </Button>
                            </div>
                        }

                        {!addressLoading && !deliveryInfo && addresses.length === 0 &&
                            <div>
                                <Button
                                    onClick={handleAddNewAddress}
                                    size='small'
                                    variant='contained'
                                    startIcon={<AddIcon />}
                                >
                                    Add new address
                                </Button>
                            </div>
                        }
                    </Section>

                    <Section>
                        <SectionTitle variant='h3'>2 Payment Method</SectionTitle>

                        <SectionContent>
                            <Avatar>
                                <CreditCardIcon />
                            </Avatar>
                            Credit card: **** 9999
                        </SectionContent>
                    </Section>

                    <Section>
                        <SectionTitle variant='h3'>3 Order Summary</SectionTitle>

                        {orderCheckoutItems.map(item =>
                            <ItemContainer key={item.product.id}>
                                <Link href={`/product/${item.product.id}`}>
                                    <Image src={item.product.images[0]} width={64} height={64} alt='product-image' />
                                </Link>

                                <ItemInfo>
                                    <ProductName>
                                        <Link href={`/product/${item.product.id}`} style={{ textDecoration: 'none', color: 'inherit' }}>
                                            {item.product.name}
                                        </Link>
                                    </ProductName>

                                    <div>
                                        <Button onClick={() => handleDeleteItem(item.product.id)} variant='contained'>Delete</Button>
                                    </div>
                                </ItemInfo>

                                <NumberSelector
                                    value={item.quantity}
                                    setValue={(newQuantity) => updateItemQuantity(item.product.id, newQuantity)}
                                />

                                <Price>{currencyFormat(item.product.discountedPrice * item.quantity)}</Price>
                            </ItemContainer>
                        )}
                    </Section>
                </SectionContainer>

                <PriceContainer>
                    <PriceContainerTitle>Purchase summary</PriceContainerTitle>
                    <Divider />
                    <PriceContainerContent>
                        <PriceContainerRow>
                            <span>Products ({orderCheckoutItems.length})</span>
                            <span>{currencyFormat(totalAmount)}</span>
                        </PriceContainerRow>
                        <PriceContainerRow>
                            <span>Shipping</span>
                            <span>{currencyFormat(shipping)}</span>
                        </PriceContainerRow>
                        <TotalPrice>
                            <span>Total</span>
                            <span>{currencyFormat(totalAmount + shipping)}</span>
                        </TotalPrice>

                        <LoadingButton
                            onClick={handlePlaceOrder}
                            loading={placeOrderLoading}
                            fullWidth
                            variant='contained'
                        >
                            Place Order
                        </LoadingButton>
                    </PriceContainerContent>
                </PriceContainer>
            </Container>
        </Main>
    )
}

export default function Private() {
    return (
        <PrivateRoute>
            <Checkout />
        </PrivateRoute>
    )
}
