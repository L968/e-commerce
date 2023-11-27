import Link from "@/components/Link";
import { BreadcrumbItem, Breadcrumbs, Card, CardContainer, Header, Title } from "./styles";

import NavigateNextIcon from '@mui/icons-material/NavigateNext';

export default function Shop() {
    return (
        <main>
            <Header>
                <Title variant='h6'>Shop</Title>
                <Breadcrumbs separator={<NavigateNextIcon fontSize="small" />}>
                    <Link href='/home'>
                        <BreadcrumbItem>Home</BreadcrumbItem>
                    </Link>
                    <span>Shop</span>
                </Breadcrumbs>
            </Header>
            <CardContainer>
                <Card>CLOTHS</Card>
                <Card>CLOTHS</Card>
                <Card>CLOTHS</Card>
                <Card>CLOTHS</Card>
                <Card>CLOTHS</Card>
            </CardContainer>
        </main>
    )
}
