import { styled } from '@mui/system';
import Typography from '@mui/material/Typography';
import Link from '../Link';

export const StyledBreadcrumbItem = styled(Typography)({
    color: '#252B42',
    fontWeight: 600,
})

export interface BreadcrumbItemProps {
    href: string;
    text: string;
}

export default function BreadcrumbItem({ href, text }: BreadcrumbItemProps) {
    return (
        <Link href={href}>
            <StyledBreadcrumbItem>{text}</StyledBreadcrumbItem>
        </Link>
    )
}
