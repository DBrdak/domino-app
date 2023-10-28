import React, {useCallback} from 'react'
import {useDropzone} from 'react-dropzone'
import {Upload} from "@mui/icons-material";
import {Typography} from "@mui/material";

interface Props {
    setFiles: (files: any) => void
}

export default function PhotoWidgetDropzone({setFiles}: Props) {
    const dzStyles = {
        border: 'dashed 3px #E1E1E1',
        borderColor: '#eee',
        borderRadius: '5px',
        textAlign: 'center' as 'center',
        height: 75,
        width: 75,
        display:'flex',
        justifyContent: 'center',
        alignItems: 'center',
        cursor: 'pointer'
    }

    const dzActive = {
        borderColor: 'green'
    }

    const onDrop = useCallback((acceptedFiles: any) => {
        setFiles(acceptedFiles.map((file: any) => Object.assign(file, {
            preview: URL.createObjectURL(file)
        })))
    }, [setFiles])
    const {getRootProps, getInputProps, isDragActive} = useDropzone({onDrop})

    return (
        <div {...getRootProps()} style={isDragActive ? {...dzStyles, ...dzActive} : dzStyles}>
            <input {...getInputProps()} />
            <Upload color={'primary'}/>
            <Typography content='Upuść zdjęcie' />
        </div>
    )
}