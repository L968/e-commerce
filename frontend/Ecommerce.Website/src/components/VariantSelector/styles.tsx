import { styled } from '@mui/system';
import Box from '@mui/material/Box';
import MuiChip from '@mui/material/Chip';

interface ChipProps {
    isValidOption: boolean
    isSelected: boolean
}

export const Chip = styled(MuiChip, {
    shouldForwardProp: prop => prop !== 'isValidOption' && prop !== 'isSelected'
})<ChipProps>(({ isValidOption, isSelected }) => ({
    backgroundColor: '#FFF',
    cursor: 'pointer',
    borderWidth: isSelected ? 2 : 1,
    borderColor: isSelected ? '#3483FA' : '#757575',
    borderStyle: isValidOption ? 'solid' : 'dashed',
    color: isValidOption ? 'inherit' : '#757575',
}))

export const TitleContainer = styled(Box)({
    marginBottom: '8px',
})

export const Title = styled('span')({
    fontWeight: 600
})
