import React, {useCallback, useState} from "react";
import {useDropzone} from "react-dropzone";
import {Upload} from "@mui/icons-material";
import {Button, Select, Stack, Typography} from "@mui/material";

interface PriceListUploadModalProps {
    onUpload: (file: File) => any
}

export function PriceListUploadModal({onUpload}: PriceListUploadModalProps) {
    const [selectedFile, setSelectedFile] = useState<File | null>(null);

    const dzStyles = {
        border: 'dashed 3px #E1E1E1',
        borderColor: '#eee',
        borderRadius: '5px',
        textAlign: 'center' as 'center',
        height: 200,
        width: '100%',
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
        cursor: 'pointer',
    };

    const dzActive = {
        borderColor: 'green',
    };

    const onDrop = useCallback((acceptedFiles: File[]) => {
        // Since you are using react-dropzone, acceptedFiles is an array
        if (acceptedFiles.length > 0) {
            setSelectedFile(acceptedFiles[0]);
        }
    }, [setSelectedFile]);

    const { getRootProps, getInputProps, isDragActive } = useDropzone({
        onDrop,
        multiple: false,
        accept: {key: ['.xlsx']},
    });

    return (
        <Stack direction={'column'} spacing={4} justifyContent={'center'}>
            <div>
                <Typography variant="h5" textAlign={'center'}>Prześlij arkusz cennika</Typography>
                <Typography variant={'caption'} textAlign={'center'}>*Cennik musi mieć format tabeli 2-kolumnowej z danymi w kolumnach A i B</Typography>
            </div>
            <div {...getRootProps()} style={isDragActive ? { ...dzStyles, ...dzActive } : dzStyles}>
                <input {...getInputProps()} />
                <Upload />
            </div>
            <Button
                variant="contained"
                color="primary"
                disabled={!selectedFile}
                onClick={() => selectedFile && onUpload(selectedFile)}
            >
                Prześlij
            </Button>
        </Stack>
    );
}