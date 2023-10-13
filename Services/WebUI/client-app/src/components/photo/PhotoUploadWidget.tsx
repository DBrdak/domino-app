import React, { useEffect, useState } from 'react'
import {IconButton, Stack} from "@mui/material";
import PhotoWidgetDropzone from "./PhotoUploadDropzone";
import PhotoWidgetCropper from "./PhotoCropper";
import {Check, CheckBox, CheckCircle, CheckOutlined} from "@mui/icons-material";

interface Props {
    uploadPhoto:(file: Blob) => void
}

function PhotoUploadWidget({uploadPhoto}: Props) {
    const [files, setFiles] = useState<any>([])
    const [cropper, setCropper] = useState<Cropper>()

    function onCrop() {
        if(cropper && cropper.getCroppedCanvas()) {
            cropper.getCroppedCanvas().toBlob(blob => uploadPhoto(blob!))
        }
    }

    useEffect(() => {
        return () => {
            files.forEach((file: any) => URL.revokeObjectURL(file.preview))
        }
    }, [files])


    return (
        <Stack direction={'column'} spacing={2} style={{display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
            <Stack direction={'row'} spacing={2} style={{display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                <PhotoWidgetDropzone setFiles={setFiles} />
                {files && files.length > 0 && (
                    <Stack direction={'row'} spacing={5}>
                        <PhotoWidgetCropper setCropper={setCropper} imagePreview={files[0].preview} />
                        <div className='img-preview' style={{ minHeight: 200, minWidth:200, overflow: 'hidden' }}/>
                    </Stack>
                )}
            </Stack>
            {files.length > 0 &&
                (<IconButton color={'secondary'} style={{backgroundColor: '#C32B28', borderRadius: 10, width: '75px'}}
                         onClick={() => onCrop()}>
                    <Check/>
                </IconButton>)
            }
        </Stack>
    )
}

export default PhotoUploadWidget