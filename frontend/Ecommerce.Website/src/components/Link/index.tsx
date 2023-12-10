import { styled } from '@mui/system';
import NextLink, { LinkProps } from 'next/link';

interface CustomLinkProps extends LinkProps {
    children: React.ReactNode;
}

const StyledLink = styled(NextLink)({
    textDecoration: 'none',
    color: 'inherit',
    transition: '0.3s',
    '&:hover': {
        filter: 'brightness(60%)'
    }
})

export default function Link({ href, children }: CustomLinkProps) {
    return (
        <StyledLink href={href}>
            {children}
        </StyledLink>
    )
}
