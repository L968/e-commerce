import { styled } from '@mui/system';
import { Box, Grid, IconButton } from '@mui/material';

interface ContainerProps {
    isDragAccept: boolean
    isDragReject: boolean
}

const getColor = (props: ContainerProps) => {
    if (props.isDragAccept) {
        return '#00E676';
    }

    if (props.isDragReject) {
        return '#FF1744';
    }

    return '#FFF';
}

export const Container = styled('section')<ContainerProps>(({ isDragAccept, isDragReject }) => ({
    flex: 1,
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    marginBottom: '12px',
    height: '107px',
    padding: '20px',
    borderWidth: '3px',
    borderRadius: '2px',
    borderColor: getColor({ isDragAccept, isDragReject }),
    borderStyle: 'dashed',
    color: '#BDBDBD',
    fontSize: '18px',
    transition: '.15s ease-in-out',
    '&:hover': {
        cursor: 'pointer',
        filter: 'brightness(1.3)',
    },
    '&:drop': {
        cursor: 'pointer',
    },
}))

export const ContainerMessage = styled(Box)({
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
})

export const ImagesContainer = styled(Grid)({
    display: 'flex',
    gap: '4%'
})

export const ImageContainer = styled(Grid)({
    position: 'relative',
})

export const ImageCloseButton = styled(IconButton)({
    position: 'absolute',
    top: 0,
    right: 0,
    zIndex: 1,
})

export const Image = styled('img')({
    filter: 'brightness(.7)',
    maxWidth: '100px',
    minWidth: '100px',
    height: '100px',
    borderRadius: '6px',
})
