import { useDropzone } from 'react-dropzone';
import CancelIcon from '@mui/icons-material/Cancel';
import SaveAltIcon from '@mui/icons-material/SaveAlt';
import { Container, ContainerMessage, Image, ImageCloseButton, ImageContainer, ImageCoverDiv, ImagesContainer } from './styles';

interface DropzoneProps {
    files: File[]
    setFiles: (files: File[]) => void
    onDrop: (files: File[]) => void
}

export default function Dropzone({ files, setFiles, onDrop }: DropzoneProps) {
    const {
        getRootProps,
        getInputProps,
        isDragActive,
        isDragAccept,
        isDragReject,
    } = useDropzone({
        accept: { 'image/*': [] },
        onDropAccepted: onDrop
    });

    function renderDragMessage() {
        if (!isDragActive) {
            return (
                <ContainerMessage>
                    <SaveAltIcon />
                    <div>Choose a file or drag it here</div>
                </ContainerMessage>
            )
        }

        if (isDragReject) {
            return <p>File not supported</p>
        }

        return <p>Drop your image</p>
    }

    function handleOnRemoveImage(file: File) {
        const newFiles = [...files];
        newFiles.splice(newFiles.indexOf(file), 1);
        setFiles(newFiles);
    }

    return (
        <>
            <Container
                {...getRootProps()}
                isDragAccept={isDragAccept}
                isDragReject={isDragReject}
            >
                <input {...getInputProps()} />
                {renderDragMessage()}
            </Container>

            <ImagesContainer container>
                {files.map((file, i) => (
                    <ImageContainer key={i} item xs={2}>
                        <ImageCloseButton onClick={() => handleOnRemoveImage(file)} size='small'>
                            <CancelIcon />
                        </ImageCloseButton>

                        {i === 0 && <ImageCoverDiv>COVER IMAGE</ImageCoverDiv>}

                        <Image src={URL.createObjectURL(file)} alt={file.name} />
                    </ImageContainer>
                ))}
            </ImagesContainer>
        </>
    )
}
