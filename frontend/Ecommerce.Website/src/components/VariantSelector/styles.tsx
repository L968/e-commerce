import { styled } from '@mui/system';
import MuiChip from '@mui/material/Chip';
import { Box } from '@mui/material';

interface ChipProps {
    isValidOption: boolean;
}

export const Chip = styled(MuiChip)<ChipProps>(({ isValidOption }) => ({
    cursor: 'pointer',
    borderWidth: 1,
    borderColor: '#757575',
    borderStyle: isValidOption ? 'solid' : 'dashed',
    color: isValidOption ? 'inehrit' : '#757575',
}))

export const TitleContainer = styled(Box)({
    marginBottom: '8px',
})

export const Title = styled('span')({
    fontWeight: 600
})
