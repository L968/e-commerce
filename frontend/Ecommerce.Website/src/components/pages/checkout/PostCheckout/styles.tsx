import { styled } from '@mui/system';
import { Box, Typography } from '@mui/material';

export const Container = styled(Box)({
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    minHeight: '700px',
    backgroundColor: '#EEE',
})

export const SuccessBanner = styled(Box)({
    display: 'flex',
    justifyContent: 'center',
    width: '100vw',
    height: '170px',
    backgroundColor: '#2DA552',
})

export const BannerContainer = styled(Box)({
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'flex-start',
    width: '550px',
    paddingTop: '40px',
})

export const Title = styled(Typography)({
    color: '#FFF',
    fontWeight: 600,
    fontSize: '20px',
}) as typeof Typography

export const DetailsContainer = styled(Box)({
    marginTop: '-35px',
    width: '550px',
    backgroundColor: '#FFF',
    borderRadius: '6px'
})

export const DetailsTitle = styled(Box)({
    padding: '20px',
    borderBottom: '1px solid rgba(0, 0, 0, .1)',
    '& h2': {
        fontSize: '18px',
        margin: 0
    },
})

export const DetailsAction = styled(Box)({
    padding: '20px',
})
