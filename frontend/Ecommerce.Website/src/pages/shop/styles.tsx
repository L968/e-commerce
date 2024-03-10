import { styled } from '@mui/system';
import Box from '@mui/material/Box';
import Grid from '@mui/material/Grid';
import Select from '@mui/material/Select';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';

export const Header = styled(Box)({
    display: 'flex',
    alignItems: 'center',
    height: '64px',
    padding: '0 80px 0 24px',
    backgroundColor: '#FAFAFA',
})

export const Title = styled(Typography)({
    color: '#252B42',
    marginRight: '100px',
    fontWeight: 600,
}) as typeof Typography

export const CategoriesCardsArea = styled(Grid)({
    display: 'flex',
    justifyContent: 'center',
    marginBottom: '48px',
})

export const Card = styled(Grid)({
    height: '223px',
    width: '205px',
    display: 'flex',
    flexDirection: 'column',
    justifyContent: 'center',
    alignItems: 'center',
    margin: '0 10px',
    cursor: 'pointer',
    '& img': {
        filter: 'brightness(0.8)',
        transition: 'filter 0.2s',
    },
    '& img:hover': {
        filter: 'brightness(1)',
    },
})

export const CardTextContainer= styled('div')({
    position: 'absolute',
})

export const CardTitle = styled('div')({
    fontSize: '16px',
    fontWeight: 600,
    color: '#FFF',
    marginBottom: '10px',
})

export const CardQuantity = styled('div')({
    fontSize: '14px',
    fontWeight: 400,
    textAlign: 'center',
    color: '#FFF',
})

export const FilterArea = styled(Box)({
    display: 'flex',
    padding: '0px 195px',
    justifyContent: 'center',
    alignItems: 'center',
    alignSelf: 'stretch',
})

export const FilterContainer = styled(Box)({
    display: 'flex',
    width: '1049px',
    justifyContent: 'space-between',
    alignItems: 'center',
    gap: '80px',
    padding: '24px 0',
})

export const FilterSort = styled(Box)({
    display: 'flex',
    padding: '0px 1px',
    alignItems: 'center',
    gap: '15px',
})

export const FilterViewsText = styled('span')({
    fontWeight: 600,
    color: '#737373'
})

export const FilterIconContainer = styled(Box)({
    display: 'flex',
    padding: '15px',
    alignItems: 'center',
    borderRadius: '5px',
    border: '1px solid #ECECEC',
})

export const FilterResultsText = styled(Typography)({
    fontSize: '14px',
    fontWeight: 600,
    color: '#737373'
})

export const FilterInputContainer = styled(Box)({
    display: 'flex',
    padding: '0px 1px',
    alignItems: 'center',
    gap: '15px',
})

export const PopularitySelect = styled(Select)({
    backgroundColor: '#F9F9F9',
    width: '141px',
})

export const FilterButton = styled(Button)({
    width: '94px',
    alignSelf: 'stretch',
    fontWeight: 600
})

export const ProductCardsArea = styled(Box)({
    display: 'flex',
    padding: '0px 158px',
    flexDirection: 'column',
    justifyContent: 'center',
    alignItems: 'center',
})

export const ProductCardsContainer = styled(Box)({
    display: 'flex',
    width: '1124px',
    padding: '48px 0px',
    flexDirection: 'column',
    alignItems: 'center',
    gap: '48px',
})

export const ProductCardsRow = styled(Grid)({
    display: 'flex',
    alignItems: 'flex-start',
})
