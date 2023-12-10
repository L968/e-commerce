import { styled } from '@mui/system';
import NavigateNextIcon from '@mui/icons-material/NavigateNext';
import BreadcrumbItem, { BreadcrumbItemProps } from './BreadcrumbItem';
import MuiBreadcrumbs, { BreadcrumbsProps } from "@mui/material/Breadcrumbs";

export interface CustomBreadcrumbsProps extends BreadcrumbsProps {
    items: BreadcrumbItemProps[];
}

const StyledBreadcrumbs = styled(MuiBreadcrumbs)({
    marginLeft: 'auto',
    '& span': {
        color: '#BDBDBD',
        fontWeight: 600,
    }
})

export default function Breadcrumbs({ items }: CustomBreadcrumbsProps) {
    return (
        <StyledBreadcrumbs separator={<NavigateNextIcon fontSize="small" />}>
            {items.map((item, i) => (
                i !== items.length - 1
                    ? <BreadcrumbItem key={i} href={item.href} text={item.text} />
                    : <span key={i}>{item.text}</span>
            ))}
        </StyledBreadcrumbs>
    )
}
