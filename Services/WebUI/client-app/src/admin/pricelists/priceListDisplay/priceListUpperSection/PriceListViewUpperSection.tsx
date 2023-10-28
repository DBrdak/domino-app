import {Box, Button, Stack} from "@mui/material";
import PriceListCreateModal from "../../priceListCreation/PriceListCreateModal";
import {AddCircleOutlined, Save, UploadFile} from "@mui/icons-material";
import React from "react";
import {observer} from "mobx-react-lite";
import LineItemCreateModal from "./LineItemCreateModal";
import {useStore} from "../../../../global/stores/store";
import {PriceListUploadModal} from "./PriceListUploadModal";
import LoadingComponent from "../../../../components/LoadingComponent";

interface Props {
    priceListId: string
}

function PriceListViewUpperSection({priceListId}: Props) {
    const {modalStore, adminPriceListStore} = useStore()

    function handleDownload() {
        adminPriceListStore.downloadPriceList(priceListId)
    }

    function handleUpload() {
        modalStore.openModal(<PriceListUploadModal onUpload={(file: File) => {
            modalStore.closeModal()
            adminPriceListStore.uploadPriceList(file, priceListId)
        } } />)
    }

    return (
        <Stack width={'100%'} direction={'row'} justifyContent={'space-between'}>
            <Stack direction={'row'} spacing={3} >
                <Button variant={'contained'} disabled={adminPriceListStore.downloadLoading || adminPriceListStore.selectedPriceList?.lineItems.length === 0}
                        style={{display: 'flex', gap: '10px'}} color={'info'} onClick={handleDownload}>
                    {adminPriceListStore.downloadLoading ?
                        <LoadingComponent />
                        :
                        <>
                            <Save />
                            Pobierz cennik
                        </>
                    }
                </Button>
                <Button variant={'outlined'} disabled={adminPriceListStore.uploadLoading} style={{display: 'flex', gap: '10px'}} color={'info'} onClick={handleUpload}>
                    {adminPriceListStore.uploadLoading ?
                        <LoadingComponent />
                        :
                        <>
                            <UploadFile />
                            Prześlij produkty
                        </>
                    }
                </Button>
            </Stack>
            <Button color='primary' variant={'contained'} style={{display: 'flex', gap: '10px'}}
                    onClick={() => modalStore.openModal(<LineItemCreateModal priceListId={priceListId} />)}>
                <AddCircleOutlined fontSize='medium' />
                Dodaj nowy produkt
            </Button>
        </Stack>
    );
}

export default observer(PriceListViewUpperSection)