import Box from '@mui/material/Box';
import Grid from '@mui/material/Grid';
import TwitterIcon from '@mui/icons-material/Twitter';
import FacebookIcon from '@mui/icons-material/Facebook';
import InstagramIcon from '@mui/icons-material/Instagram';
import { BrandName, EmailTextField, FooterCategory, FooterContent, FooterCredits, FooterHeader, FooterSubCategory, SocialMediaIcons, SubscribeButton } from './styles';

export default function Footer() {
    return (
        <Box>
            <FooterHeader boxShadow={1}>
                <BrandName>Brand Name</BrandName>
                <SocialMediaIcons>
                    <FacebookIcon />
                    <InstagramIcon />
                    <TwitterIcon />
                </SocialMediaIcons>
            </FooterHeader>
            <FooterContent>
                <Grid container>
                    <Grid item xs={2}>
                        <FooterCategory>Company Info</FooterCategory>
                        <FooterSubCategory>About Us</FooterSubCategory>
                        <FooterSubCategory>Carrier</FooterSubCategory>
                        <FooterSubCategory>We are hiring</FooterSubCategory>
                        <FooterSubCategory>Blog</FooterSubCategory>
                    </Grid>
                    <Grid item xs={2}>
                        <FooterCategory>Legal</FooterCategory>
                        <FooterSubCategory>About Us</FooterSubCategory>
                        <FooterSubCategory>Carrier</FooterSubCategory>
                        <FooterSubCategory>We are hiring</FooterSubCategory>
                        <FooterSubCategory>Blog</FooterSubCategory>
                    </Grid>
                    <Grid item xs={2}>
                        <FooterCategory>Features</FooterCategory>
                        <FooterSubCategory>Business Marketing</FooterSubCategory>
                        <FooterSubCategory>User Analytic</FooterSubCategory>
                        <FooterSubCategory>Live Chat</FooterSubCategory>
                        <FooterSubCategory>Unlimited Support</FooterSubCategory>
                    </Grid>
                    <Grid item xs={2}>
                        <FooterCategory>Resources</FooterCategory>
                        <FooterSubCategory>IOS & Android</FooterSubCategory>
                        <FooterSubCategory>Watch a Demo</FooterSubCategory>
                        <FooterSubCategory>Customers</FooterSubCategory>
                        <FooterSubCategory>API</FooterSubCategory>
                    </Grid>
                    <Grid item xs={4}>
                        <FooterCategory>Get in Touch</FooterCategory>
                        <EmailTextField label='Your Email' />
                        <SubscribeButton variant='contained'>Subscribe</SubscribeButton>
                    </Grid>
                </Grid>
            </FooterContent>
            <FooterCredits>
                Made by Lucas Constantino
            </FooterCredits>
        </Box>
    );
}
